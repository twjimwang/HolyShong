namespace HolyShong.HolyShongModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductOption")]
    public partial class ProductOption
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductOption()
        {
            ProductOptionDetail = new HashSet<ProductOptionDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductOptionId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsNecessary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOptionDetail> ProductOptionDetail { get; set; }
    }
}
