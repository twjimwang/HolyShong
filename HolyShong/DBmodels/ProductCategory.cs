namespace HolyShong.DBmodels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductCategory()
        {
            Product = new HashSet<Product>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCategoryId { get; set; }

        public int StoreId { get; set; }

        public int Sort { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsEnable { get; set; }

        public bool IsDelete { get; set; }

        [Required]
        [StringLength(50)]
        public string SupplyTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Product { get; set; }

        public virtual Store Store { get; set; }
    }
}
