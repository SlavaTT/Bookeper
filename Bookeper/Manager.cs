using System;
using System.Linq;

namespace Bookeper
{
    public class Manager : ManagerBase
    {
        public const decimal SUBORDINATE_PERCENTS = 0.5M;
        public new const int EXPERIENCE_PERCENTS_PER_YEAR = 5;
        public new const int EXPERIENCE_MAX_PERCENTS_PER_YEAR = 40;

        public Manager( string name, DateTime works_from ) : base( name, works_from )
        {
        }

        public override int Level => 2;

        protected override decimal ExperiencePercentsPerYear => EXPERIENCE_PERCENTS_PER_YEAR;

        protected override decimal ExperienceMaxPercents => EXPERIENCE_MAX_PERCENTS_PER_YEAR;

        public override decimal CalcSalary()
        {
            var salary_as_employee = base.CalcSalary();

            var subordinate_percents = Subordinates
                                           .Where( s => s.Level == 1 )
                                           .Sum( s => s.CalcSalary() ) / 100 * SUBORDINATE_PERCENTS;

            return salary_as_employee + subordinate_percents;
        }
    }
}