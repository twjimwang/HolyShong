namespace HolyShong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        public int ProductCategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsPopular { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string Img { get; set; }

        public bool IsEnable { get; set; }

        public bool IsDelete { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
