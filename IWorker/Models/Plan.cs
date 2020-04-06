using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Models
{
    public class Plan
    {
        public long Id{get;set;}
        [Required]
        [MaxLength(10)]
        public string UserID { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayFormat(DataFormatString = "{d/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(50)]
        public string WorkName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Sector { get; set; }
        [Required]
        [MaxLength(15)]
        public string Hours { get; set; }
    }
}
