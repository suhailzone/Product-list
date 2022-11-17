using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductList.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string CategoryName { get; set; }

        public System.Nullable<DateTime> CreatedTimestamp { get; set; }

        public System.Nullable<DateTime> ModifiedTimestamp { get; set; }

        public System.Nullable<DateTime> DeletedTimestamp { get; set; }
    }
}
