using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Dto
{
    public class MessageDto
    {
        public long MessageID { get; set; }
        public ShortUserDto Worker { get; set; }
        public string Date { get; set; }
    }
}
