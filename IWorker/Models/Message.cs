using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Models
{
    public class Message
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(5)]
        public int From { get; set; }
        [Required]
        [MaxLength(5)]
        public int To { get; set; }
        [Required]
        [MaxLength(300)]
        public string MessageText { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayFormat(DataFormatString = "{d/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

    }
}
