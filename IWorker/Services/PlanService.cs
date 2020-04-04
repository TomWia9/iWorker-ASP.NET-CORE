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

        public PlanDetailsDto Get(string userID, DateTime date)
        {
            var plan = _context.Plans.Where(x=> x.UserID == userID && x.Date == date).SingleOrDefault();

            return new PlanDetailsDto
            {
                UserID = plan.UserID,
                WorkName = plan.WorkName,
                Sector = plan.Sector,
                Hours = plan.Hours,
                Date = plan.Date
            };
        }

        public void CreatePlan(PlanDetailsDto plan)
        {
            var newPlan = new Plan
            {
                UserID = plan.UserID,
                WorkName = plan.WorkName,
                Sector = plan.Sector,
                Hours = plan.Hours,
                Date = plan.Date,

            };

            _context.Plans.Add(newPlan);
            _context.SaveChanges();
        }

    }
}
