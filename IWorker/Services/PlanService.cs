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

        public UserPlanDetailsDto Get(int userID, string date)
        {

            var plan = _context.Plans.Where(x => x.UserID == userID && x.Date.Date == DateTime.Parse(date).Date).FirstOrDefault();

            if (plan == null)
            {
                return null;
            }

            return new UserPlanDetailsDto
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
                foreach (ShortUserDto worker in sector.Workers)
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

        public PlanDetailsDto GetFullPlan(string date) 
        {

            PlanDetailsDto fullPlan = new PlanDetailsDto();
            List<SectorPlanDto> sectorPlans = new List<SectorPlanDto>();
            List<SectorDto> sectors = new List<SectorDto>();

            var plan = _context.Plans.Where(x => x.Date.Date == DateTime.Parse(date).Date).ToList(); //here is list of users plan: id, userID, date, workName, sectorName, hours

            if (!plan.Any())
            {
                return null;
            }

            fullPlan.Date = plan.ElementAt(0).Date.ToString();
            fullPlan.Hours = plan.ElementAt(0).Hours;

            foreach (var worker in plan)
            {
                //here I create worker from data from list "plan"
                ShortUserDto user = new ShortUserDto();
                SectorDto sector = new SectorDto();
                user.UserID = worker.UserID;
                var nameAndSurname = _context.Users.Where(x => x.UserId == worker.UserID).FirstOrDefault();
                user.Name = nameAndSurname.Name;
                user.Surname = nameAndSurname.Surname;
                sector.ID = 0;
                sector.SectorName = worker.Sector;
                sector.WorkName = worker.WorkName;

                //if ther's no sectors or sector that worker is assigned hadn't created yet,
                //then create another sector in "sectors" list and then add it to "sectorPlans" list together with worker
                bool contains = false;
                foreach (SectorDto sec in sectors) 
                {
                    if(sec.SectorName == sector.SectorName)
                    {
                        contains = true;
                    }
                }

                if (!contains) //!sectors.Contains(sector) doesnt work, i dont know why
                {
                    sectors.Add(sector);
                    sectorPlans.Add(new SectorPlanDto()
                    {
                        Sector = sector,
                        Workers = new List<ShortUserDto>()
                        {
                           user
                        }
                    });
                }

                //if sector that worker is assigned for is created, then find index of sector that worker is assigned for on the "sectors" list
                //and add worker to "workers" list in "sectorPlans" list
                else
                {
                    int i = 0;
                    int index = 0;
                    foreach (SectorDto sec in sectors) //may be simplified but findIndex does not work, i don't know why
                    {

                        if (sec == sector)
                        {
                            index = i;
                        }

                        i++;
                    }

                    sectorPlans.ElementAt(index).Workers.Add(user);
                }
            }

            fullPlan.Sectors = sectorPlans;
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
