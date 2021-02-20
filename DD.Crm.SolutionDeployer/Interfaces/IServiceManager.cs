using DD.Crm.SolutionDeployer.Models;
using DD.Crm.SolutionDeployer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Crm.SolutionDeployer.Interfaces
{
    public interface IServiceManager
    {

        void ExportSolution(ContextService context, string id, ExportSolutionReq request);

        void ImportSolutionsAsync(ContextService context, string id, ImportSolutionReq request);




    }
}
