﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Models
{
    public class Report
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(10)]
        public int UserID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
        [Required]
        public string Sector { get; set; }
        [Required]
        public string WorkName { get; set; }
        public double Amount { get; set; }
        [Required]
        [MaxLength(2)]
        public double Hours { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayFormat(DataFormatString = "{d/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    
    }
}
