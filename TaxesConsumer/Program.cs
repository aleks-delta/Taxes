using System;
using System.Collections.Generic;

namespace TaxesConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Loading cities tax data");
            var scheduleDB = new Taxes.TaxScheduleDatabase();
            try
            {
                scheduleDB.LoadFromFile("../../../Taxes/city_taxes.txt");
                Console.Out.WriteLine("press key to get tax for Kaunas on 1 July 2016");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Incorrect file format: {0}\n Press any key to exit", ex.Message);
                Console.ReadKey();
                return;
            }

            double kaunasSummerRate = 0.0;
            try
            {
                kaunasSummerRate = scheduleDB.GetTaxForCity("Kaunas", new DateTime(2016, 7, 1));
                Console.WriteLine("Kaunas summer rate = {0}", kaunasSummerRate);
            }
            catch(KeyNotFoundException ex)
            {
                Console.Out.WriteLine("{0}\n Press any key to exit", ex.Message);
                Console.ReadKey();
                return;
            }

            Console.Out.WriteLine("press key to get add a 'Halloween tax' to Vilnius");
            Console.ReadKey();
            var haloweenTax = new Taxes.TaxScheduleEntryDaily(2016, 10, 31, 1.5);
            scheduleDB.AddTaxToCity("Vilnius", haloweenTax);
            var haloweenFromDB = scheduleDB.GetTaxForCity("Vilnius", new DateTime(2016, 10, 31));
            Console.WriteLine("Halloween rate for Vilnius from the DB is {0}!!!", haloweenFromDB);


            Console.Out.WriteLine("press key to get Kaunas and Vilnius rates for May 1");
            Console.ReadKey();

            var mayDay = new DateTime(2016, 5, 1);
            var KaunasMayRate = scheduleDB.GetTaxForCity("Kaunas", mayDay);
            var VilniusMayRate = scheduleDB.GetTaxForCity("Vilnius", mayDay);

            Console.Out.WriteLine("May 1: Kaunas rate = {0}, Vilnius rate = {1}", KaunasMayRate, VilniusMayRate);
            Console.Out.WriteLine("press key to get Prague rate for New Year 2018");

            Console.ReadKey();

            var nyDay = new DateTime(2018, 1, 1);
            var pragueNYRate = scheduleDB.GetTaxForCity("Prague", nyDay);

            Console.Out.WriteLine("The New Year 2018 rate for Prague is {0}", pragueNYRate);
            
            Console.Out.WriteLine("Press key to exit consumer program");
            Console.ReadKey();
        }
    }
}
