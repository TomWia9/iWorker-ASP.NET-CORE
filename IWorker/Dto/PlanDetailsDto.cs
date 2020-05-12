using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Dto
{
    public class PlanDetailsDto
    {
        public string Date { get; set; }
        public string Hours { get; set; }
        public List<SectorPlanDto> Sectors {get;set;}

        public PlanDetailsDto()
        {
            Sectors = new List<SectorPlanDto>();
         
        }
    }
}
