using System.ComponentModel.DataAnnotations;

namespace ITOS_LEARN.Models
{
    public class Permission
    {
        [Key]
        public string PermissionId { get; set; }
        public int? UserId { get; set; }
        public string RoleId { get; set; }
        public string MenuId { get; set; }
        public string ActionId { get; set; }
        public bool IsAllow { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
        public Menu Menu { get; set; }
        public MenuAction Action { get; set; }
    }
}
