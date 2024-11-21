using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITOS_LEARN.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePermissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    ActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.ActionId);
                    table.ForeignKey(
                        name: "FK_Actions_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId");
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsAllow = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "ActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "User_ID");
                });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "ActionId", "ActionName", "Description", "MenuId" },
                values: new object[,]
                {
                    { "A001", "Search", "ดูข้อมูลใน DB ว่ามีการลงทะเบียนไว้แล้วหรือยัง และ ตรวจสอบข้อมูลที่ซ้ำกันใน DB", null },
                    { "A002", "Add", "เพิ่มข้อมูลการลงทะเบียน ลง DB", null },
                    { "A003", "Edit", "แก้ไขข้อมูลของ people ในระบบ", null },
                    { "A004", "Delete", "ลบข้อมูลของ people ในระบบ", null },
                    { "A005", "NoAction", "ไม่สามารถทำอะไรได้", null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "Description", "MenuName" },
                values: new object[,]
                {
                    { "M001", "เข้าสู่หน้า Home", "Home" },
                    { "M002", "เข้าสู่หน้า Register", "Register" },
                    { "M003", "เข้าสู่หน้า Login", "Login" },
                    { "M004", "เข้าสู่หน้า Waiting", "Waiting" },
                    { "M005", "เข้าสู่หน้า Detail", "Detail" },
                    { "M006", "เข้าสู่หน้า Admin Approval", "Admin Approval" },
                    { "M007", "เข้าสู่หน้า Add New Person", "Add New Person" },
                    { "M008", "เข้าสู่หน้า Edit", "Edit" },
                    { "M009", "เข้าสู่หน้า Delete", "Delete" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { "R001", "Admin" },
                    { "R002", "Creater" },
                    { "R003", "Viewer" },
                    { "R004", "User" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ActionId", "IsAllow", "MenuId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { "P001", "A001", true, "M001", "R001", null },
                    { "P002", "A001", true, "M002", "R001", null },
                    { "P003", "A002", true, "M002", "R001", null },
                    { "P004", "A001", false, "M003", "R001", null },
                    { "P005", "A002", true, "M003", "R001", null },
                    { "P006", "A001", true, "M005", "R001", null },
                    { "P007", "A002", true, "M005", "R001", null },
                    { "P008", "A003", true, "M005", "R001", null },
                    { "P009", "A004", true, "M005", "R001", null },
                    { "P010", "A002", true, "M006", "R001", null },
                    { "P011", "A002", true, "M007", "R001", null },
                    { "P012", "A003", true, "M008", "R001", null },
                    { "P013", "A004", true, "M009", "R001", null },
                    { "P014", "A005", false, "M005", "R002", null },
                    { "P015", "A002", true, "M007", "R002", null },
                    { "P016", "A003", true, "M008", "R003", null },
                    { "P017", "A004", true, "M009", "R003", null },
                    { "P018", "A005", true, "M001", "R004", null },
                    { "P019", "A005", true, "M004", "R004", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_MenuId",
                table: "Actions",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ActionId",
                table: "Permissions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_MenuId",
                table: "Permissions",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
