using System;
using System.Collections.Generic;

namespace Bookeper
{
    public abstract class ManagerBase : Employee
    {
        private readonly List<Employee> _subordinates;

        public ManagerBase( string name, DateTime works_from ) : base( name, works_from )
        {
            _subordinates = new List<Employee>();
        }

        public IEnumerable<Employee> Subordinates => _subordinates;

        public void AddSubordinate( Employee worker )
        {
            if ( worker == this )
                throw new ApplicationException( "Cannot make an employee a subordinate of itself" );
            if ( worker.Boss != null )
                throw new ApplicationException( $"This employee is already subordinate of {worker.Boss.Name}" );
            _subordinates.Add( worker );
            worker.Boss = this;
        }

        public void RemoveSubordinate( Employee worker )
        {
            _subordinates.Remove( worker );
            worker.Boss = null;
        }
    }
}