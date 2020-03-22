using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task1{
    class WebCrowler{
        static async Task Main(string[] args) {

            var websiteURL = args.Length > 0 ? args[0] : throw new ArgumentNullException();
            var httpClient = new HttpClient();

            try{
                var response = await httpClient.GetAsync(websiteURL);

                httpClient.Dispose();

                if (response.IsSuccessStatusCode)
                {
                    var htmlContent = await response.Content.ReadAsStringAsync();

                    var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                    var emailAddresses = regex.Matches(htmlContent);

                    if(emailAddresses.Count > 0) {
                        foreach (var email in emailAddresses)
                        {
                            Console.WriteLine(email.ToString());
                        }
                    }else
                    {
                        Console.WriteLine("No email addresses found");
                    }

                    
                }

            }catch(Exception)
            {
                Console.WriteLine("Error while downloading the page");

            }

            Console.ReadKey();
        }
    }
}