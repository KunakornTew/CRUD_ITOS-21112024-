using Microsoft.EntityFrameworkCore;
using ITOS_LEARN.Models;

namespace ITOS_LEARN.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuAction> Actions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Seed Data สำหรับ Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = "R001", RoleName = "Admin" },
                new Role { RoleId = "R002", RoleName = "Creater" },
                new Role { RoleId = "R003", RoleName = "Viewer" },
                new Role { RoleId = "R004", RoleName = "User" }
            );

            // Seed Data สำหรับ Menus
            modelBuilder.Entity<Menu>().HasData(
                new Menu { MenuId = "M001", MenuName = "Home", Description = "เข้าสู่หน้า Home" },
                new Menu { MenuId = "M002", MenuName = "Register", Description = "เข้าสู่หน้า Register" },
                new Menu { MenuId = "M003", MenuName = "Login", Description = "เข้าสู่หน้า Login" },
                new Menu { MenuId = "M004", MenuName = "Waiting", Description = "เข้าสู่หน้า Waiting" },
                new Menu { MenuId = "M005", MenuName = "Detail", Description = "เข้าสู่หน้า Detail" },
                new Menu { MenuId = "M006", MenuName = "Admin Approval", Description = "เข้าสู่หน้า Admin Approval" },
                new Menu { MenuId = "M007", MenuName = "Add New Person", Description = "เข้าสู่หน้า Add New Person" },
                new Menu { MenuId = "M008", MenuName = "Edit", Description = "เข้าสู่หน้า Edit" },
                new Menu { MenuId = "M009", MenuName = "Delete", Description = "เข้าสู่หน้า Delete" }
            );

            // Seed Data สำหรับ MenuActions
            modelBuilder.Entity<MenuAction>().HasData(
            new MenuAction { ActionId = "A001", ActionName = "Search", Description = "ดูข้อมูลใน DB ว่ามีการลงทะเบียนไว้แล้วหรือยัง และ ตรวจสอบข้อมูลที่ซ้ำกันใน DB" },
            new MenuAction { ActionId = "A002", ActionName = "Add", Description = "เพิ่มข้อมูลการลงทะเบียน ลง DB" },
            new MenuAction { ActionId = "A003", ActionName = "Edit", Description = "แก้ไขข้อมูลของ people ในระบบ" },
            new MenuAction { ActionId = "A004", ActionName = "Delete", Description = "ลบข้อมูลของ people ในระบบ" },
            new MenuAction { ActionId = "A005", ActionName = "NoAction", Description = "ไม่สามารถทำอะไรได้" }
            );

            modelBuilder.Entity<Permission>().HasData(
            new Permission { PermissionId = "P001", UserId = null, RoleId = "R001", MenuId = "M001", ActionId = "A001", IsAllow = true },
            new Permission { PermissionId = "P002", UserId = null, RoleId = "R001", MenuId = "M002", ActionId = "A001", IsAllow = true },
            new Permission { PermissionId = "P003", UserId = null, RoleId = "R001", MenuId = "M002", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P004", UserId = null, RoleId = "R001", MenuId = "M003", ActionId = "A001", IsAllow = false },
            new Permission { PermissionId = "P005", UserId = null, RoleId = "R001", MenuId = "M003", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P006", UserId = null, RoleId = "R001", MenuId = "M005", ActionId = "A001", IsAllow = true },
            new Permission { PermissionId = "P007", UserId = null, RoleId = "R001", MenuId = "M005", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P008", UserId = null, RoleId = "R001", MenuId = "M005", ActionId = "A003", IsAllow = true },
            new Permission { PermissionId = "P009", UserId = null, RoleId = "R001", MenuId = "M005", ActionId = "A004", IsAllow = true },
            new Permission { PermissionId = "P010", UserId = null, RoleId = "R001", MenuId = "M006", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P011", UserId = null, RoleId = "R001", MenuId = "M007", ActionId = "A002", IsAllow = true },
            new Permission { PermissionId = "P012", UserId = null, RoleId = "R001", MenuId = "M008", ActionId = "A003", IsAllow = true },
            new Permission { PermissionId = "P013", UserId = null, RoleId = "R001", MenuId = "M009", ActionId = "A004", IsAllow = true },

            // Creater - สามารถเข้าถึงหน้า Detail และ Add New Person ได้
            new Permission { PermissionId = "P014", UserId = null, RoleId = "R002", MenuId = "M005", ActionId = "A005", IsAllow = false }, // Detail - ไม่มีสิทธิ์ทำ Action
            new Permission { PermissionId = "P015", UserId = null, RoleId = "R002", MenuId = "M007", ActionId = "A002", IsAllow = true }, // Add New Person

            // Editor - สามารถทำ Action Edit และ Delete ในหน้า Edit และ Delete
            new Permission { PermissionId = "P016", UserId = null, RoleId = "R003", MenuId = "M008", ActionId = "A003", IsAllow = true }, // Edit
            new Permission { PermissionId = "P017", UserId = null, RoleId = "R003", MenuId = "M009", ActionId = "A004", IsAllow = true }, // Delete

            // User - สามารถเข้าถึงหน้า Home และ Waiting โดยไม่มีสิทธิ์ทำ Action
            new Permission { PermissionId = "P018", UserId = null, RoleId = "R004", MenuId = "M001", ActionId = "A005", IsAllow = true }, // Home - ไม่มีสิทธิ์ทำ Action
            new Permission { PermissionId = "P019", UserId = null, RoleId = "R004", MenuId = "M004", ActionId = "A005", IsAllow = true } // Waiting - ไม่มีสิทธิ์ทำ Action
            );

            // กำหนดความสัมพันธ์ระหว่างตาราง
            modelBuilder.Entity<Permission>()
                .Property(p => p.UserId)
                .IsRequired(false);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Menu)
                .WithMany(m => m.Permissions)
                .HasForeignKey(p => p.MenuId);

            modelBuilder.Entity<Permission>()
                .HasOne(p => p.Action)
                .WithMany(a => a.Permissions)
                .HasForeignKey(p => p.ActionId);

            modelBuilder.Entity<Login>()
                .Property(l => l.ID)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง ID

            modelBuilder.Entity<User>()
                .Property(u => u.User_ID)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง User_ID

            modelBuilder.Entity<Role>()
                .Property(r => r.RoleId)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง RoleId

            modelBuilder.Entity<Menu>()
                .Property(m => m.MenuId)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง MenuId

            modelBuilder.Entity<MenuAction>()
                .Property(a => a.ActionId)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง ActionId

            modelBuilder.Entity<Permission>()
                .Property(p => p.PermissionId)
                .ValueGeneratedOnAdd(); // ให้ฐานข้อมูลจัดการการสร้าง PermissionId
        }
    }
}