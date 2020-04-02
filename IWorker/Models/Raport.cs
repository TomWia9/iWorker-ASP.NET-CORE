using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Models
{
    public class Raport
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public string WorkName { get; set; }
        public string Sector { get; set; }
        public string Amount { get; set; }
        public string Hours { get; set; }
        public string Date { get; set; }
        public string Chests { get; set; }
    }
}
