using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Dto
{
    public class DataStatisticsDto
    {
        public double Max { get; set; }
        public double Total { get; set; }
        public double Min { get; set; }
        public double Avg { get; set; }
        public double AvgMonth { get; set; }
        public double AvgWeek { get; set; }
    }
}
