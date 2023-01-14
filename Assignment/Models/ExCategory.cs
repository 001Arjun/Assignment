using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Assignment.Models
{
    [Table("tbl_Category")]
    public class ExCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "*Name Required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "*Limit Set Required..")]
        public int Ex_Limit { get; set; }
    }
}