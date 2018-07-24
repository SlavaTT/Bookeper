using System;
using System.Linq;
using NUnit.Framework;

namespace Bookeper
{
    [TestFixture]
    public class BookeperTest
    {
        private const int YEARS_SERVED = 5;

        private Employee _joe;
        private Employee _nick;
        private Employee _steve;

        private Manager _chris;
        private Sales _john;


        [OneTimeSetUp]
        public void Setup()
        {
            _joe = new Employee( "Joe", DateTime.Now.AddYears( -YEARS_SERVED ) );
            _nick = new Employee( "Nick", DateTime.Now.AddYears( -YEARS_SERVED ) );
            _steve = new Employee( "Steve", DateTime.Now.AddYears( -YEARS_SERVED ) );

            _chris = new Manager( "Chris", DateTime.Now.AddYears( -YEARS_SERVED ) );
            _john = new Sales( "John", DateTime.Now.AddYears( -YEARS_SERVED ) );

            _chris.AddSubordinate( _joe );
            _chris.AddSubordinate( _nick );
            _chris.AddSubordinate( _steve );

            _john.AddSubordinate( _chris );
        }

        [Test]
        public void EmployeeTest()
        {
            var correct_salary = Employee.BASE_MONTH_SALARY +
                                 Employee.BASE_MONTH_SALARY / 100 *
                                 ( YEARS_SERVED * Employee.EXPERIENCE_PERCENTS_PER_YEAR );

            Assert.AreEqual( _nick.CalcSalary(), correct_salary );
        }

        [Test]
        public void ManagerTest()
        {
            var salary = Employee.BASE_MONTH_SALARY +
                         Employee.BASE_MONTH_SALARY / 100 *
                         ( YEARS_SERVED * Manager.EXPERIENCE_PERCENTS_PER_YEAR );

            var employee_salary = Employee.BASE_MONTH_SALARY +
                                  Employee.BASE_MONTH_SALARY / 100 *
                                  ( YEARS_SERVED * Employee.EXPERIENCE_PERCENTS_PER_YEAR );
            var sub_total_salary = employee_salary * _chris.Subordinates.Count();
            var sub_percents = sub_total_salary / 100 * Manager.SUBORDINATE_PERCENTS;

            var correct_salary = salary + sub_percents;

            Assert.AreEqual( _chris.CalcSalary(), correct_salary );
        }

        [Test]
        public void SalesTest()
        {
            var salary = Employee.BASE_MONTH_SALARY +
                         Employee.BASE_MONTH_SALARY / 100 *
                         ( YEARS_SERVED * Sales.EXPERIENCE_PERCENTS_PER_YEAR );

            var sub_total_salary = _chris.CalcSalary();
            var sub_percents = sub_total_salary / 100 * Sales.SUBORDINATE_PERCENTS;

            var correct_salary = salary + sub_percents;

            Assert.AreEqual( _john.CalcSalary(), correct_salary );
        }
    }
}