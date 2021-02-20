using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Crm.SolutionDeployer.Services
{
    public class ContextService
    {

        public Dictionary<string, object> ContextValues { get; set; }

        public ContextService()
        {
            ContextValues = new Dictionary<string, object>();
        }



        public void AddContextValue<T>(string name, T value)
        {
            ContextValues.Add(name, value);
        }
    }
}
