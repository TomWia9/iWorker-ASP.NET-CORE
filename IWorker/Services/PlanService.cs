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

            var plan = _context.Plans.Where(x => x.UserID == userID && x.Date.Date == DateTime.Parse(date).Date).FirstOrDefault();

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
            if (_context.Plans.Where(x => x.Date.Date == DateTime.Parse(plan.Date).Date).Any() || DateTime.Parse(plan.Date).Date < DateTime.Now.Date)
            {
                return false; //worker cant add 2 plans for same day, also he cant add plan for past days
            }

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

        public PlanDetailsDto GetFullPlan(string date)
        {

            PlanDetailsDto fullPlan = new PlanDetailsDto();
            
            var plan = _context.Plans.Where(x => x.Date.Date == DateTime.Parse(date).Date).ToList();

            if (!plan.Any())
            {
                return null;
            }

            fullPlan.Date = plan[0].Date.ToString();
            fullPlan.Hours = plan[0].Hours;

            foreach (var worker in plan)
            {
                UsersListDto user = new UsersListDto();
                user.UserID = worker.UserID;
                var nameAndSurname = _context.Users.Where(x => x.UserId == worker.UserID).FirstOrDefault();
                user.Name = nameAndSurname.Name;
                user.Surname = nameAndSurname.Surname;

                switch (worker.Sector)
                {
                    case "A1":
                        {
                            fullPlan.A1.Add(user);
                            break;
                        }

                    case "B12":
                        {
                            fullPlan.B12.Add(user);
                            break;
                        }

                    case "EZ":
                        {
                            fullPlan.EZ.Add(user);
                            break;
                        }

                    case "ES":
                        {
                            fullPlan.ES.Add(user);
                            break;
                        }

                    case "C3":
                        {
                            fullPlan.C3.Add(user);
                            break;
                        }

                    case "H12":
                        {
                            fullPlan.H12.Add(user);
                            break;
                        }
                }
            }

            return fullPlan;

        }

        public List<PlanDateDto> GetListOfPlanDates()
        {
            bool canAdd = true;

            List<PlanDateDto> planDates = new List<PlanDateDto>();

           List<PlanDateDto> allPlanDates =  _context.Plans.Where(x => x.Date >= DateTime.Now.Date).Select(x => new PlanDateDto
           {
               Date = x.Date.ToShortDateString()
           }).ToList();

            foreach (var item in allPlanDates)
            {
                canAdd = true;

                foreach (var planDate in planDates)
                {
                    if (planDate.Date == item.Date)
                    {
                        canAdd = false;
                    }
                }

                if (canAdd)
                {
                    planDates.Add(item);
                }
               
            }

            return planDates;

        }

        public bool DeletePlan(string date)
        {
            var plans = _context.Plans.Where(x => x.Date == DateTime.Parse(date).Date).ToList();
            if (plans.Any())
            {
                foreach (var plan in plans)
                {
                    _context.Plans.Remove(plan);
                }
               
                _context.SaveChanges();
                return false; //error = false
            }

            return false; //error = true
        }

    }
 
}
