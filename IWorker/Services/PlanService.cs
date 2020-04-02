using IWorker.Dto;
using IWorker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Services
{
    public class PlanService
    {
        private readonly IWorkerContext _context;

        public PlanService(IWorkerContext context)
        {
            _context = context;
        }

        public PlanDetailsDto Get(string userID, string date)
        {
            var plan = _context.Plans.Include(x => x.UserID).SingleOrDefault(x => x.UserID == userID && x.Date == date);

            return new PlanDetailsDto
            {
                WorkName = plan.WorkName,
                Sector = plan.Sector,
                Hours = plan.Hours,
            };
        }

    }
}
