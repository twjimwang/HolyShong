namespace HolyShong.DBmodels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discount")]
    public partial class Discount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discount()
        {
            DiscountMember = new HashSet<DiscountMember>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DiscountId { get; set; }

        public int CartId { get; set; }

        [Required]
        [StringLength(20)]
        public string DiscountCode { get; set; }

        [Required]
        [StringLength(20)]
        public string DisplayName { get; set; }

        public int Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int UseLimit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountMember> DiscountMember { get; set; }
    }
}
