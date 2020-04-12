using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Dto
{
    public class PlanDetailsDto
    {
        public int UserID { get; set; }
        public string WorkName { get; set; }
        public string Sector { get; set; }
        public string Hours { get; set; }
        public string Date { get; set; }
    }
}
