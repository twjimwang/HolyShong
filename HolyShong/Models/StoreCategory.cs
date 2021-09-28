namespace HolyShong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StoreCategory")]
    public partial class StoreCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StoreCategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string KeyWord { get; set; }

        [Required]
        public string Img { get; set; }
    }
}
