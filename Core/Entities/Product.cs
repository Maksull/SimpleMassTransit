using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Core.Entities
{
    public sealed class Product
    {
        [Key]
        [DataMember(Name = "productId")]
        public Guid ProductId { get; set; }
        [Required]
        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [DataMember(Name = "price")]
        public decimal Price { get; set; }
        [Required]
        [ForeignKey(nameof(CategoryId))]
        [DataMember(Name = "categoryId")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
