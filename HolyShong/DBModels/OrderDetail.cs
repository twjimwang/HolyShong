namespace HolyShong.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int ProductOptionId { get; set; }

        public int ProductOptionDetailId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
    }
}
