namespace HolyShong.DBmodels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductOption")]
    public partial class ProductOption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductOptionId { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsNecessary { get; set; }

        public virtual Product Product { get; set; }
    }
}
