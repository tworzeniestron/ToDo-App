using System.ComponentModel.DataAnnotations.Schema;

namespace MettecApi.Models
{
    [Table("MettecItems")]
    public class MettecItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
