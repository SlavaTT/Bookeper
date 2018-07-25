using System;
using NUnit.Framework;

namespace Bookeper
{
    [TestFixture]
    public class BookeperTest
    {
        private Employee _joe;
        private Employee _nick;
        private Employee _steve;

        private Manager _chris;
        private Sales _john;

        private const int JOE_YEARS = 1;
        private const int NICK_YEARS = 10;
        private const int STEVE_YEARS = 15;
        private const int CHRIS_YEARS = 2;
        private const int JOHN_YEARS = 2;


        [OneTimeSetUp]
        public void Setup()
        {
            _joe = new Employee( "Joe", DateTime.Now.AddYears( -JOE_YEARS ) );
            _nick = new Employee( "Nick", DateTime.Now.AddYears( -NICK_YEARS ) );
            _steve = new Employee( "Steve", DateTime.Now.AddYears( -STEVE_YEARS ) );

            _chris = new Manager( "Chris", DateTime.Now.AddYears( -CHRIS_YEARS ) );
            _john = new Sales( "John", DateTime.Now.AddYears( -JOHN_YEARS ) );

            _chris.AddSubordinate( _joe );
            _chris.AddSubordinate( _nick );
            _chris.AddSubordinate( _steve );

            _john.AddSubordinate( _chris );
        }

        private decimal CalcEmployeeSalary( int years_served )
        {
            var percents_year = years_served * Employee.EXPERIENCE_PERCENTS_PER_YEAR;
            if ( percents_year > Employee.EXPERIENCE_MAX_PERCENTS_PER_YEAR )
                percents_year = Employee.EXPERIENCE_MAX_PERCENTS_PER_YEAR;
            var salary = Employee.BASE_MONTH_SALARY +
                         Employee.BASE_MONTH_SALARY / 100 * percents_year;

            return salary;
        }

        [Test]
        public void EmployeeTest()
        {
            var correct_salary = CalcEmployeeSalary( STEVE_YEARS );

            Assert.AreEqual( _steve.CalcSalary(), correct_salary );

            Assert.AreEqual( _steve.CalcSalary(), _nick.CalcSalary() );
        }

        [Test]
        public void ManagerTest()
        {
            var base_salary = Employee.BASE_MONTH_SALARY +
                              Employee.BASE_MONTH_SALARY / 100 *
                              ( CHRIS_YEARS * Manager.EXPERIENCE_PERCENTS_PER_YEAR );

            var joe_salary = CalcEmployeeSalary( JOE_YEARS );
            var steve_salary = CalcEmployeeSalary( STEVE_YEARS );
            var nick_salary = CalcEmployeeSalary( NICK_YEARS );

            var sub_total_salary = joe_salary + steve_salary + nick_salary;

            var sub_percents = sub_total_salary / 100 * Manager.SUBORDINATE_PERCENTS;

            var correct_salary = base_salary + sub_percents;

            Assert.AreEqual( _chris.CalcSalary(), correct_salary );
        }

        [Test]
        public void SalesTest()
        {
            var salary = Employee.BASE_MONTH_SALARY +
                         Employee.BASE_MONTH_SALARY / 100 *
                         ( JOHN_YEARS * Sales.EXPERIENCE_PERCENTS_PER_YEAR );

            var sub_total_salary = _chris.CalcSalary();
            var sub_percents = sub_total_salary / 100 * Sales.SUBORDINATE_PERCENTS;

            var correct_salary = salary + sub_percents;

            Assert.AreEqual( _john.CalcSalary(), correct_salary );
        }
    }
}