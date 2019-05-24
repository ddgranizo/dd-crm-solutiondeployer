using DD.Crm.SolutionDeployer.Models;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Crm.SolutionDeployer.Providers
{
    public static class CrmProvider
    {



        public static void ExportSolution(IOrganizationService service, bool managed, string uniqueName, string path)
        {
            ExportSolutionRequest req = new ExportSolutionRequest()
            {
                Managed = managed,
                SolutionName = uniqueName,
            };
            var response = (ExportSolutionResponse)service.Execute(req);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllBytes(path, response.ExportSolutionFile);
        }

        public static Guid ImportSolutionAsync(
                IOrganizationService service, 
                byte[] data, 
                bool overwriteUnmanagedCustomizations = true,
                bool migrateAsHold = false,
                bool publishWorkflows = true)
        {
            ImportSolutionRequest importRequest = new ImportSolutionRequest()
            {
                CustomizationFile = data,
                OverwriteUnmanagedCustomizations = overwriteUnmanagedCustomizations,
                HoldingSolution = migrateAsHold,
                PublishWorkflows = publishWorkflows,
            };

            ExecuteAsyncRequest asyncRequest = new ExecuteAsyncRequest()
            {
                Request = importRequest,

            };
            var asyncResponse = (ExecuteAsyncResponse)service.Execute(asyncRequest);
            var asyncJobId = asyncResponse.AsyncJobId;
            return asyncJobId;
        }




        public static string GetStringConnection(CrmConnection connection)
        {
            return string.Format("ServiceUri={0}; Username={1}; Password={2}; authtype=Office365; RequireNewInstance=True;",
                                    connection.Url,
                                    connection.Username,
                                    connection. Password);
        }

        public static IOrganizationService GetService(CrmConnection connection)
        {
            return GetService(GetStringConnection(connection));

        }
        public static IOrganizationService GetService(string stringConnection)
        {
            CrmServiceClient crmService = new CrmServiceClient(stringConnection);
            IOrganizationService serviceProxy = crmService.OrganizationWebProxyClient != null ?
                                                        crmService.OrganizationWebProxyClient :
                                                        (IOrganizationService)crmService.OrganizationServiceProxy;
            return serviceProxy;
        }

    }
}
