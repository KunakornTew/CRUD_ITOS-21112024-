using System.ComponentModel.DataAnnotations;

namespace ITOS_LEARN.Models
{
    public class Role
    {
        [Key]
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
