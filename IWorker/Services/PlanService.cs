using IWorker.Dto;
using IWorker.Models;
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
           
            var plan = _context.Plans.Where(x => x.UserID == userID && x.Date.Date == DateTime.Parse(date).Date).SingleOrDefault();

            if(plan == null)
            {
                return null;
            }

            return new PlanDetailsDto
            {
                UserID = plan.UserID,
                WorkName = plan.WorkName,
                Sector = plan.Sector,
                Hours = plan.Hours,
                Date = plan.Date.ToShortDateString()
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
                Date = DateTime.Parse(plan.Date), 

            };

            _context.Plans.Add(newPlan);
            _context.SaveChanges();
        }

    }
}
