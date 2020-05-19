using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Dto
{
    public class ShortReportDto
    {
        public long ID { get; set; } //reports id not user
        public string Date { get; set; }
        public string WorkName { get; set; }
    }
}
