using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIM.DR
{
        
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var token = AuthClient.GetToken();

                if (APIM.Config.Config.PrintToken)
                {
                    Console.WriteLine(token.AccessToken);
                }

                string method = "backup";
                if (args.Length > 0 && args[0] == "restore")
                {
                    method = "restore";
                }

                System.Console.Write("serviceName: {0}\nbackupName: {1}. \noperation to be performed: {2}\nconfirm? [y/n]:", APIM.Config.Config.ServiceName, APIM.Config.Config.BackupName, method);
                if (System.Console.ReadLine() == "y")
                {
                    System.Console.WriteLine();
                    var apiClient = new ApiClient(token.AccessToken);

                    var task = Task.Run(async () => { await apiClient.SendRequest(method); });
                    
                    while (task.Status == TaskStatus.Running || task.Status == TaskStatus.WaitingForActivation)
                    {
                        System.Console.Write(".");
                        System.Threading.Thread.Sleep(500);
                    }
                }

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            };

            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadKey();
        }
    }
}
