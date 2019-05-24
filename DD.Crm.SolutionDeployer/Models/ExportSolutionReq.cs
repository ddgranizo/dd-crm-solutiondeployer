using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Crm.SolutionDeployer.Models
{
    public class ExportSolutionReq
    {
        public string Path { get; set; }
        public string UniqueName { get; set; }
        public bool Managed { get; set; }
        public ExportSolutionReq( string uniqueName, bool managed, string path)
        {
            this.Path = path;
            this.Managed = managed;
            this.UniqueName  = uniqueName;
        }
    }
}
