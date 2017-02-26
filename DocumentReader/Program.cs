using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DocumentReader
{
    class Program
    {
        public static string pathToFile { get; set; }
        public delegate void ShowProgressDel();
        public static void ShowProgress()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(200);
                Console.Write(".");
            }
        }

        static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            string nameSpaceName = "DocumentReader"; 

            var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), "Test doc.txt");
            string[] data = File.ReadAllLines(pathToFile); //source file with test data

            foreach (string line in data)
            {
                Thread.Sleep(300);

                string[] separatedData = line.Split(' '); // split data by white spaces
                var separatedDataLIST = separatedData.ToList(); // convert to list cause i want to remove extra symbols from source file 
                separatedDataLIST.Remove("");
                separatedDataLIST.Remove(",");

                int countOfElements = separatedDataLIST.Count;


                // get clear data from previous list
                string[] clearData = new string[countOfElements]; 
                int j = 0;
                foreach (string data1 in separatedDataLIST)
                {
                    clearData[j] = data1;
                    j++;
                }
                           

                string className = clearData[0]; // returns Square, so class is Square, Circle, Rectangle, Triangle, Ellipse 
                double[] parameters = new double[clearData.Length - 1]; // new array for parameters (sizes of figures)
                for (int i = 1; i < clearData.Length; i++)
                {

                    try
                    {
                        parameters[i - 1] = Convert.ToDouble(clearData[i].TrimEnd(',')); // in case when multi parameters, separated by ',' : 5,6, where 5,6 is size
                    }
                    catch
                    {
                        parameters[i - 1] = Convert.ToDouble(clearData[i]);// in case when single parameters
                    }


                }

                try
                {
                    //if name of Class is incorrect we sent this exception to catch block, not to UnhandledExceptionTrapper
                    var newInstance = Activator.CreateInstance(Type.GetType(nameSpaceName + "." + className),
                                     new Object[] { parameters, clearData[0] });

                    // if instance constructor is not available it returns null
                    Figure figure = (Figure)newInstance;
                    if (figure.message == null)
                    {
                        throw new Exception();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error in input parameters: {0}", line);
                }


            }
            Task.WaitAll();
           // ShowProgress();



        }

        // in case when we have incorrect data like "5/" or "5|"
        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            Console.WriteLine(exception.Message.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            
            Environment.Exit(1);
        }



    }
}
