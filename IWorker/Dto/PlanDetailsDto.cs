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
        public List<UsersListDto> A1 { get; set; }
        public List<UsersListDto> B12 { get; set; }
        public List<UsersListDto> EZ { get; set; }
        public List<UsersListDto> ES { get; set; }
        public List<UsersListDto> C3 { get; set; }
        public List<UsersListDto> H12 { get; set; }

        public PlanDetailsDto()
        {
            A1 = new List<UsersListDto>();
            B12 = new List<UsersListDto>();
            EZ = new List<UsersListDto>();
            ES = new List<UsersListDto>();
            C3 = new List<UsersListDto>();
            H12 = new List<UsersListDto>();
        }
    }
}
