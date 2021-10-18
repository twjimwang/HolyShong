namespace HolyShong.Models.HolyShongModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ECPayRecord")]
    public partial class ECPayRecord
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ECPayRecordID { get; set; }

        public int? OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TradeNo { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsPaySuccess { get; set; }

        public string Payment { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime TradeTime { get; set; }

        [Key]
        [Column(Order = 4)]
        public string CheckMacValue { get; set; }

        public virtual Order Order { get; set; }
    }
}
