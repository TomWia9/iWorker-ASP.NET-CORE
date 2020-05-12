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

        private void AddPlan(SectorPlanDto sector, string hours, string date)
        {
            if (sector.Workers.Any())
            {
                foreach (UsersListDto worker in sector.Workers)
                {
                    var newPlan = new Plan
                    {
                        UserID = worker.UserID,
                        WorkName = sector.Sector.WorkName,
                        Sector = sector.Sector.SectorName,
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
                return false; //employer cant add 2 plans for same day, also he cant add plan for past days
            }

            try
            {
                foreach (SectorPlanDto sector in plan.Sectors)
                {
                    AddPlan(sector, plan.Hours, plan.Date);
                }

                return true;
            }

            catch
            {
                return false;
            }
          
        }

        public PlanDetailsDto GetFullPlan(string date) //doesnt work right now
        {

            PlanDetailsDto fullPlan = new PlanDetailsDto();
            
            var plan = _context.Plans.Where(x => x.Date.Date == DateTime.Parse(date).Date).ToList();

            if (!plan.Any())
            {
                return null;
            }

            fullPlan.Date = plan.ElementAt(0).Date.ToString();
            fullPlan.Hours = plan.ElementAt(0).Hours;

            foreach (var worker in plan)
            {
                UsersListDto user = new UsersListDto();
                user.UserID = worker.UserID;
                var nameAndSurname = _context.Users.Where(x => x.UserId == worker.UserID).FirstOrDefault();
                user.Name = nameAndSurname.Name;
                user.Surname = nameAndSurname.Surname;



                //fullPlan.Sectors.Add(new SectorPlanDto()  //mehh
                //{
                //    Sector = new SectorDto()
                //    {
                //        SectorName = worker.Sector,
                //        WorkName = worker.WorkName,
                //    },
                //    Workers = worker

                //}) ;


                //switch (worker.Sector)
                //{
                //    case "A1":
                //        {
                //            fullPlan.A1.Add(user);
                //            break;
                //        }

                //    case "B12":
                //        {
                //            fullPlan.B12.Add(user);
                //            break;
                //        }

                //    case "EZ":
                //        {
                //            fullPlan.EZ.Add(user);
                //            break;
                //        }

                //    case "ES":
                //        {
                //            fullPlan.ES.Add(user);
                //            break;
                //        }

                //    case "C3":
                //        {
                //            fullPlan.C3.Add(user);
                //            break;
                //        }

                //    case "H12":
                //        {
                //            fullPlan.H12.Add(user);
                //            break;
                //        }
                //}
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
