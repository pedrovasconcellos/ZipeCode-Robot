using ZipeCodeConsole.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ZipeCodeConsole
{
    class Program
    {
        private static DAL dal = new DAL();
        private static DateTime clock = DateTime.Now;
        private readonly static Object objectLocked = new Object();

        static void Main(string[] args)
        {
            try
            {
                Task taskProcess = new Task(delegate { Process(); });
                taskProcess.Start();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Task taskPrintColor = PrintColor(ex.Message, ConsoleColor.Red);
                Console.ReadKey();
            }
        }

        private static void Process()
        {
            var lastZipeCodeInsert = (new DAL().LastZipeCodeInsert() ?? 0) + 1;
            var zipeCodeMax = 99999999;

            Task taskSearchingPrintColor = PrintColor(
                String.Format("{0} || Searching from the ZipeCode = {1}", DateTime.Now, lastZipeCodeInsert), ConsoleColor.Yellow);

            while (lastZipeCodeInsert <= zipeCodeMax)
            {
                Task taskZipeCodePrintColor = PrintColor((String.Concat("CEP: ", lastZipeCodeInsert.ToString(@"00000\-000"))), (ConsoleColor)new Random().Next(7, 14));
                ProcessCep(lastZipeCodeInsert.ToString().PadLeft(8, '0'));
                lastZipeCodeInsert++;
            }

            Task taskFinishedPrintColor = PrintColor("Finished Process.", ConsoleColor.Yellow);

        }

        private static void ProcessCep(string zipeCode)
        {
            if (!(dal.ExistZipeCode<object>(new ZipeCode { cep = zipeCode })))
            {
                ZipeCode objCep = CEPRequest.GetZipeCodeInfo(zipeCode);
                objCep.datetime = DateTime.Now;

                clockTime();
                if (objCep.estado != null && objCep.cidade != null)
                {
                    Task taskInsert = dal.Insert<object>(objCep);
                    Task taskPrintObject = PrintObject(objCep);
                }
                else
                {
                    Task taskZipeCodeNotFoundPrintColor = PrintColor($"ZipeCode {Convert.ToInt32(zipeCode).ToString(@"00000\-000")} not found!", ConsoleColor.Yellow);
                }
            }

        }

        private static void clockTime()
        {
            int miliseconds = DateTime.Now.Subtract(clock).Milliseconds;
            clock = DateTime.Now;
            int waitTime = (4000 - miliseconds);
            if (waitTime > 0) { Thread.Sleep(waitTime); }
        }

        private static async Task PrintObject(object obj)
        {
            await Task.Run(() =>
            {
                string response = new JavaScriptSerializer().Serialize(obj);
                Task printColor = PrintColor(response, ConsoleColor.Green);

            });
        }

        private static async Task PrintColor(string text, ConsoleColor fontColor)
        {
            await Task.Run(() =>
            {
                lock (objectLocked)
                {
                    Console.ForegroundColor = fontColor;
                    Console.WriteLine(String.Concat(text, Environment.NewLine));
                }
            });
        }
    }
}
