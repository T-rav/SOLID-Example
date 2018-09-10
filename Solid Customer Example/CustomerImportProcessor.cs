using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Solid_Customer_Example
{
    // allow for importing customers from a web service? for LSP
    public class CustomerImportProcessor
    {
        // todo : correct the DIP violation and the SRP violation
        public List<Customer> Create_Customers_From_File()
        {
            var path = "customer_import.txt";
            var customerLines = File.ReadAllLines(path);
            var customers = new List<Customer>();

            foreach (var customer in customerLines)
            {
                var customerParts = customer.Split(',');
                customers.Add(new Customer
                {
                    Name = customerParts[0],
                    Product = customerParts[1],
                    PurchasePrice = double.Parse(customerParts[2])
                });
            }

            return customers;
        }

        public List<Customer> Create_Customers_From_Web_Service()
        {
            var url = "https://raw.githubusercontent.com/T-rav/solid_customer_example_webservice/master/db.json";
            using (var client = new WebClient())
            {
                var jsonData = client.DownloadString(url);
                return JsonConvert.DeserializeObject<List<Customer>>(jsonData);
            }
        }
    }
}
