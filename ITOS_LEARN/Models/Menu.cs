using System.ComponentModel.DataAnnotations;

namespace ITOS_LEARN.Models
{
    public class Menu
    {
        [Key]
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
