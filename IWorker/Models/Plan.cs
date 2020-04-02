﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWorker.Models
{
    public class Plan
    {
        public long Id{get;set;}
        public string UserID { get; set; }
        public string Date { get; set; }
        public string WorkName { get; set; }
        public string Sector { get; set; }
        public string Hours { get; set; }
    }
}
