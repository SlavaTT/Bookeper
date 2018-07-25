using System;

namespace Bookeper
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            var TestCompany = new Company( "Interpipe" );

            var Vasya = new Employee( "Vasya", DateTime.Today.AddYears( -1 ) );
            TestCompany.Workers.Add( Vasya );

            var Petya = new Employee( "Petya", DateTime.Today.AddYears( -2 ) );
            TestCompany.Workers.Add( Petya );

            var Nick = new Employee( "Nick", DateTime.Today.AddYears( -3 ) );
            TestCompany.Workers.Add( Nick );

            var SteveManager = new Manager( "Steve", DateTime.Today.AddYears( -1 ) );
            TestCompany.Workers.Add( SteveManager );

            var JohnSales = new Sales( "John", DateTime.Today.AddYears( -3 ) );
            TestCompany.Workers.Add( JohnSales );

            SteveManager.AddSubordinate( Vasya );
            SteveManager.AddSubordinate( Petya );

            JohnSales.AddSubordinate( SteveManager );
            JohnSales.AddSubordinate( Nick );

            foreach ( var Worker in TestCompany.Workers )
                Console.WriteLine( $"Worker {Worker.Name}, makes {Worker.CalcSalary()}" );

            Console.WriteLine( $"Company's total salary spendings: {TestCompany.CalcTotalSalary()}" );
        }
    }
}