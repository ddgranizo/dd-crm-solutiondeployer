using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Crm.SolutionDeployer.Models
{
    public class ImportSolutionReq
    {
        public string Path { get; set; }
        public bool PublishWorkflows { get; set; }
        public bool MigrateAsHold { get; set; }
        public bool OverwriteUnmanagedCustomizations { get; set; }
        public ImportSolutionReq(string path, bool publishWorkflows, bool migrateAsHold, bool overwriteUnmanagedCustomizations)
        {
            this.Path = path;
            this.PublishWorkflows = publishWorkflows;
            this.MigrateAsHold = migrateAsHold;
            this.OverwriteUnmanagedCustomizations = overwriteUnmanagedCustomizations;
        }
    }
}
