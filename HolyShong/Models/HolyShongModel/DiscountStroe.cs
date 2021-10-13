namespace HolyShong.Models.HolyShongModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiscountStroe")]
    public partial class DiscountStroe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DiscountStoreId { get; set; }

        public int DiscountId { get; set; }

        public int StoreId { get; set; }

        public int UsedNumber { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual Store Store { get; set; }
    }
}
