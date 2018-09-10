using System.Collections.Generic;

namespace Solid_Customer_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Run_Report_For_Clients(args[0], args[1]);
        }

        private static void Run_Report_For_Clients(string dataSource, string reportType)
        {
            var importer = new CustomerImportProcessor();
            var customers = new List<Customer>();

            // todo : remove the SRP violation
            if (dataSource == "file")
            {
                customers = importer.Create_Customers_From_File();
            }
            else if (dataSource == "web")
            {
                customers = importer.Create_Customers_From_Web_Service();
            }

            var report = new Customer()
                        .Generate_Report(customers, reportType);
        }
    }
}
