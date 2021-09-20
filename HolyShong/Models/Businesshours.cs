namespace HolyShong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Businesshours
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BusinesshoursId { get; set; }

        public int StoreId { get; set; }

        public int WeekDay { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public virtual Store Store { get; set; }
    }
}
