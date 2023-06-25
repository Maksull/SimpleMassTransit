using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Core.Entities
{
    public sealed class Category
    {
        [Key]
        [DataMember(Name = "categoryId")]
        public Guid CategoryId { get; set; }
        [Required]
        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;
    }
}
