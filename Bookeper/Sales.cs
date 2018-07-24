using System;
using System.Linq;

namespace Bookeper
{
    public class Sales : ManagerBase
    {
        public const decimal SUBORDINATE_PERCENTS = 0.3M;
        public new const int EXPERIENCE_PERCENTS_PER_YEAR = 1;
        public new const int EXPERIENCE_MAX_PERCENTS_PER_YEAR = 35;

        public Sales( string name, DateTime works_from ) : base( name, works_from )
        {
        }

        public override int Level => 3;

        protected override decimal ExperiencePercentsPerYear => EXPERIENCE_PERCENTS_PER_YEAR;

        protected override decimal ExperienceMaxPercents => EXPERIENCE_MAX_PERCENTS_PER_YEAR;

        public override decimal CalcSalary()
        {
            var salary_as_employee = base.CalcSalary();

            var subordinate_percents = Subordinates.Sum( s => s.CalcSalary() ) / 100 * SUBORDINATE_PERCENTS;

            return salary_as_employee + subordinate_percents;
        }
    }
}