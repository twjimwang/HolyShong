namespace HolyShong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductOptionDetail")]
    public partial class ProductOptionDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductOptionDetail()
        {
            ItemDetail = new HashSet<ItemDetail>();
            ItemDetail1 = new HashSet<ItemDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductOptionDetailId { get; set; }

        public int ProductOptionId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemDetail> ItemDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemDetail> ItemDetail1 { get; set; }
    }
}
