﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Dto
{
    public class RaportListDto
    {
        public long ID { get; set; } //raports id not user
        public string Date { get; set; }
        public string WorkName { get; set; }
    }
}
