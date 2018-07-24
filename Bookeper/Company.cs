using System.Collections.Generic;
using System.Linq;

namespace Bookeper
{
    public class Company
    {
        public Company( string name )
        {
            Name = name;
            Workers = new List<Employee>();
        }

        public string Name { get; }
        public List<Employee> Workers { get; }

        public decimal CalcTotalSalary()
        {
            return Workers.Sum( worker => worker.CalcSalary() );
        }
    }
}