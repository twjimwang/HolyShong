namespace HolyShong.DBmodels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AddressId { get; set; }

        public int MemberId { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int ZipCode { get; set; }

        [Column("Address")]
        [Required]
        public string Address1 { get; set; }

        public virtual Member Member { get; set; }
    }
}
