namespace HolyShong.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Item = new HashSet<Item>();
            ProductOption = new HashSet<ProductOption>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        public int ProductCategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsPopular { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string Img { get; set; }

        public bool IsEnable { get; set; }

        public bool IsDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Item { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOption> ProductOption { get; set; }
    }
}
