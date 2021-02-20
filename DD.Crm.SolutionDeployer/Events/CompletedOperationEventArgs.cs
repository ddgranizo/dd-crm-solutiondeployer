using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Crm.SolutionDeployer.Events
{
    
    public class OperationEventArgs: EventArgs
    {
        public OperationType Type { get; set; }
        public string Id { get; set; }
        
        public OperationEventArgs(
            OperationType type, 
            string id)
                : base()
        {
            this.Type = type;
            this.Id = id;
        }
    }
}
