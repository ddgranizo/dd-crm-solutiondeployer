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
        public Guid OperationId { get; set; }
        public int TotalOperationsNumber { get; set; }
        public int SequenceNumber { get; set; }
        public OperationEventArgs(
            OperationType type, 
            Guid operationId,
            int totalOperationsNumber,
            int sequeceNumber)
                : base()
        {
            this.Type = type;
            this.OperationId = operationId;
            this.TotalOperationsNumber = totalOperationsNumber;
            this.SequenceNumber = sequeceNumber;
        }
    }
}
