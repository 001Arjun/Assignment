using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Assignment.Models
{
    [Table("tbl_Expense")]
    public class Expenss
    {
        [Key]
        public int Exp_Id { get; set; }

        [Required(ErrorMessage = "*Title Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*Description Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*Amount Required")]
        public int Amount { get; set; }

        [Display(Name = "tbl_Category")]
        public virtual int Id { get; set; }

        [ForeignKey("Id")]
        public virtual ExCategory ExCat { get; set; }

        public DateTime Date { get; set; }
    }
}