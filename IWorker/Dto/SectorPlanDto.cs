﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Dto
{
    public class SectorPlanDto
    {
        public SectorDto Sector;
        public List<UsersListDto> Workers;

        public SectorPlanDto()
        {
            Workers = new List<UsersListDto>();
        }
    }
}
