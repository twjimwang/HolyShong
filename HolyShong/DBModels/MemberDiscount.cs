namespace HolyShong.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MemberDiscount")]
    public partial class MemberDiscount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MemberDiscount()
        {
            Order = new HashSet<Order>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MemberDiscountId { get; set; }

        public int DiscountId { get; set; }

        public int MemberId { get; set; }

        public bool isUsed { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
