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

        public WorkerPlanDetailsDto Get(int userID, string date)
        {

            var plan = _context.Plans.Where(x => x.UserID == userID && x.Date.Date == DateTime.Parse(date).Date).SingleOrDefault();

            if (plan == null)
            {
                return null;
            }

            return new WorkerPlanDetailsDto
            {
                UserID = plan.UserID,
                WorkName = plan.WorkName,
                Sector = plan.Sector,
                Hours = plan.Hours,
                Date = plan.Date.ToShortDateString()
            };
        }

        private string GetWorknameFromSector(string sector)
        {
            return sector switch
            {
                "A1" => "Truskawki",
                "B12" => "Jerzyny",
                "EZ" => "Maliny",
                "ES" => "Maliny",
                "C3" => "Truskawki",
                "H12" => "Borówki",
                _ => null,
            };
        }

        private void AddPlan(List<UsersListDto> sector, string sectorName, string hours, string date)
        {
            if (sector.Any())
            {
                foreach (UsersListDto worker in sector)
                {
                    var newPlan = new Plan
                    {
                        UserID = worker.UserID,
                        WorkName = GetWorknameFromSector(sectorName),
                        Sector = sectorName,
                        Hours = hours,
                        Date = DateTime.Parse(date),
                    };

                    _context.Plans.Add(newPlan);
                    _context.SaveChanges();
                }
            }
        }

        public bool CreatePlan(PlanDetailsDto plan)
        {
            try
            {
                AddPlan(plan.A1, "A1", plan.Hours, plan.Date);
                AddPlan(plan.B12, "B12", plan.Hours, plan.Date);
                AddPlan(plan.EZ, "EZ", plan.Hours, plan.Date);
                AddPlan(plan.ES, "ES", plan.Hours, plan.Date);
                AddPlan(plan.C3, "C3", plan.Hours, plan.Date);
                AddPlan(plan.H12, "H12", plan.Hours, plan.Date);
                return true;
            }

            catch
            {
                return false;
            }
          
        }
    }
 
}
