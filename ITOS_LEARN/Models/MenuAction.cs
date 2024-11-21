using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ITOS_LEARN.Models
{
    public class MenuAction
    {
        [Key]
        public string ActionId { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
        public Menu Menu { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
