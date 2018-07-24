using System;

namespace Bookeper
{
    internal class Program
    {
        private static void Main( string[] args )
        {
            var InterpipeCompany = new Company( "Interpipe" );

            var Vasya = new Employee( "Vasya", DateTime.Today.AddYears( -1 ) );
            InterpipeCompany.Workers.Add( Vasya );

            var Petya = new Employee( "Petya", DateTime.Today.AddYears( -2 ) );
            InterpipeCompany.Workers.Add( Petya );

            var Nick = new Employee( "Nick", DateTime.Today.AddYears( -3 ) );
            InterpipeCompany.Workers.Add( Nick );

            var SteveManager = new Manager( "Steve", DateTime.Today.AddYears( -1 ) );
            InterpipeCompany.Workers.Add( SteveManager );

            var JohnSales = new Sales( "John", DateTime.Today.AddYears( -3 ) );
            InterpipeCompany.Workers.Add( JohnSales );

            SteveManager.AddSubordinate( Vasya );
            SteveManager.AddSubordinate( Petya );

            JohnSales.AddSubordinate( SteveManager );
            JohnSales.AddSubordinate( Nick );

            foreach ( var Worker in InterpipeCompany.Workers )
                Console.WriteLine( $"Worker {Worker.Name}, makes {Worker.CalcSalary()}" );

            Console.WriteLine($"Company's total salary spendings: {InterpipeCompany.CalcTotalSalary()}");
        }
    }
}