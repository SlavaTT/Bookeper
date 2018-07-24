using System;

namespace Bookeper
{
    public class Employee
    {
        public const decimal BASE_MONTH_SALARY = 1000;
        public const int EXPERIENCE_PERCENTS_PER_YEAR = 3;
        public const int EXPERIENCE_MAX_PERCENTS_PER_YEAR = 30;

        public Employee( string name, DateTime works_from )
        {
            Name = name;
            WorksFrom = works_from;
        }

        public string Name { get; }
        public DateTime WorksFrom { get; }
        public Employee Boss { get; set; }

        public virtual int Level => 1;

        protected virtual decimal ExperiencePercentsPerYear => EXPERIENCE_PERCENTS_PER_YEAR;

        protected virtual decimal ExperienceMaxPercents => EXPERIENCE_MAX_PERCENTS_PER_YEAR;

        public virtual decimal CalcSalary()
        {
            var years_served = ( DateTime.Now - WorksFrom ).Days / 365;

            var percents = years_served * ExperiencePercentsPerYear;
            percents = percents > ExperienceMaxPercents ? ExperienceMaxPercents : percents;

            var salary = BASE_MONTH_SALARY + BASE_MONTH_SALARY / 100 * percents;

            return salary;
        }
    }
}