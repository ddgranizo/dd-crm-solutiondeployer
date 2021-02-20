using DD.Crm.SolutionDeployer.Events;
using DD.Crm.SolutionDeployer.Interfaces;
using DD.Crm.SolutionDeployer.Models;
using DD.Crm.SolutionDeployer.Providers;
using DD.Crm.SolutionDeployer.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DD.Crm.SolutionDeployer
{

    public enum OperationType
    {
        ImportSolutionAsync = 1,
        ExportSolution = 2,
    }


    public delegate void OperationHandler(object sender, OperationEventArgs e);
    public class ServiceManager : IServiceManager
    {

        public event OperationHandler OnOperationStarted;
        public event OperationHandler OnOperationCompleted;

        public IOrganizationService Service { get; set; }
        public ServiceManager(IOrganizationService service)
        {
            this.Service = service;
        }

        public ServiceManager(CrmConnection connection)
        {
            this.Service = CrmProvider.GetService(connection);
        }

        public void ExportSolution(ContextService context, string id, ExportSolutionReq request)
        {
            var operationEventArgs = new OperationEventArgs(OperationType.ExportSolution, id);
            OnOperationStarted?
                .Invoke(this, operationEventArgs);
            CrmProvider.ExportSolution(Service, request.Managed, request.UniqueName, request.Path);
            OnOperationCompleted?
                .Invoke(this, operationEventArgs);
        }

        public void ImportSolutionsAsync(ContextService context, string id, ImportSolutionReq request)
        {
            var operationEventArgs = new OperationEventArgs(OperationType.ImportSolutionAsync, id);
            OnOperationStarted?
                .Invoke(this, operationEventArgs);
            var data = File.ReadAllBytes(request.Path);
            var jobId = CrmProvider
                    .ImportSolutionAsync(
                        Service,
                        data,
                        request.OverwriteUnmanagedCustomizations,
                        request.MigrateAsHold,
                        request.PublishWorkflows);
            WaitAsnycOperation(jobId);
            OnOperationCompleted?
                .Invoke(this, operationEventArgs);
        }

        private void WaitAsnycOperation(Guid jobId)
        {
            int timeMaxForTimeOut = 1000 * 60 * 200;
            DateTime end = DateTime.Now.AddMilliseconds(timeMaxForTimeOut);
            bool completed = false;
            while (!completed && end >= DateTime.Now)
            {
                System.Threading.Thread.Sleep(200);
                try
                {
                    Entity asyncOperation = Service.Retrieve("asyncoperation", jobId, new ColumnSet(true));
                    var statusCode = asyncOperation.GetAttributeValue<OptionSetValue>("statuscode").Value;
                    if (statusCode == 30)
                    {
                        completed = true;
                    }
                    else if (statusCode == 21
                            || statusCode == 22
                            || statusCode == 31
                            || statusCode == 32)
                    {
                        throw new Exception(
                                string.Format(
                                    "Async oepration failed: {0} {1}",
                                    statusCode,
                                    asyncOperation.GetAttributeValue<string>("message")));
                    }
                }
                catch (TimeoutException)
                {
                    //do nothign
                }
                catch (FaultException)
                {
                    //Do nothing
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
