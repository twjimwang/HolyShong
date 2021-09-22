namespace HolyShong.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Score")]
    public partial class Score
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScoreId { get; set; }

        public int StoreId { get; set; }

        public int OrderId { get; set; }

        [Column("Score")]
        public decimal Score1 { get; set; }

        public virtual Store Store { get; set; }
    }
}
