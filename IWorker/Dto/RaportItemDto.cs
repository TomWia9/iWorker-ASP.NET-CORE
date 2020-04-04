using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Dto
{
    public class RaportItemDto
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string WorkName { get; set; }
        public string Sector { get; set; }
        public string Amount { get; set; }
        public string Hours { get; set; }
        public DateTime Date { get; set; }
        public string Chests { get; set; }

    }                                     
}
