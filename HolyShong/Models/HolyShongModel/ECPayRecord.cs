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

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TradeNo { get; set; }

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

        [StringLength(10)]
        public string MerchantID { get; set; }

        [StringLength(20)]
        public string MerchantTradeNo { get; set; }

        public string RtnMsg { get; set; }

        public decimal? TradeAmt { get; set; }

        public DateTime? PaymentDate { get; set; }

        [StringLength(20)]
        public string PaymentType { get; set; }

        public DateTime? TradeDate { get; set; }

        [StringLength(50)]
        public string CustomField1 { get; set; }

        [StringLength(50)]
        public string CustomField2 { get; set; }

        [StringLength(50)]
        public string CustomField3 { get; set; }

        [StringLength(50)]
        public string CustomField4 { get; set; }

        public decimal? PaymentTypeChargeFee { get; set; }
    }
}
