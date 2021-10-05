namespace HolyShong.Models.HolyShongModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemDetail")]
    public partial class ItemDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemDetailId { get; set; }

        public int ItemId { get; set; }

        public int? ProductOptionId { get; set; }

        public int? ProductOptionDetailId { get; set; }

        public virtual Item Item { get; set; }

        public virtual ProductOptionDetail ProductOptionDetail { get; set; }

        public virtual ProductOptionDetail ProductOptionDetail1 { get; set; }
    }
}
