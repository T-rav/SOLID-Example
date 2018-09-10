using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Solid_Customer_Example
{
    public class Customer
    {
        public string Name { get; set; }
        public string Product { get; set; }
        public double PurchasePrice { get; set; }

        public bool Validate_Customer()
        {
            return !string.IsNullOrEmpty(Name)
                   && !string.IsNullOrEmpty(Product)
                   && PurchasePrice > 0.0;
        }

        // todo : make this configurable for countries?
        public double Get_Purchase_Price_Without_Vat()
        {
            return PurchasePrice * 0.85;
        }

        // todo : correct SRP violation
        public bool Generate_Report(List<Customer> customers, string reportType)
        {
            // todo : correct OCP violation!
            if (reportType == "CSV")
            {
                var reportLines = new List<string> { "Name, Product, Price Exclude VAT, VAT Amount" };
                foreach (var customer in customers)
                {
                    reportLines.Add($"{customer.Name},{customer.Product},{customer.Get_Purchase_Price_Without_Vat()}, {customer.PurchasePrice - customer.Get_Purchase_Price_Without_Vat()}");
                }

                var reportPath = "D:\\Systems\\customers.csv";
                File.WriteAllLines(reportPath, reportLines); // todo : correct the DI violation
                return true;
            }
            else if (reportType == "HTML")
            {
                var reportPath = "D:\\Systems\\customers.html";
                using (var file = File.OpenWrite(reportPath)) // todo : correct the DI violation on Console.WriteLine
                {
                    var data = Encoding.UTF8.GetBytes("<html><body><table><tr><td>Name</td><td>Production</td><td>Price excluding VAT</td><td>VAT</td></tr>");
                    file.Write(data, 0, data.Length);
                    foreach (var customer in customers)
                    {
                        var line =
                            $"<tr><td>{customer.Name}</td><td>{customer.Product}</td><td>{customer.Get_Purchase_Price_Without_Vat()}</td><td>{customer.PurchasePrice - customer.Get_Purchase_Price_Without_Vat()}</td></tr>";
                        var lineBytes = Encoding.UTF8.GetBytes(line);
                        file.Write(lineBytes, 0, lineBytes.Length);
                    }
                    var data2 = Encoding.UTF8.GetBytes("</table></body></html>");
                    file.Write(data2, 0, data2.Length);
                    return true;
                }
            }

            Console.WriteLine("Invalid report type selected"); // todo : correct the DI violation on Console.WriteLine
            return false;
        }
    }
}