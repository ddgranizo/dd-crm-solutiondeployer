﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Crm.SolutionDeployer.Models
{
    public class CrmConnection
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public CrmConnection()
        {

        }
    }
}
