using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentReader
{


    public abstract class Figure
    {
        public DateTime startOfExecuting = DateTime.Now;
        public string message;
        public abstract double GetSquare();

        public void  WriteToFile(string message)
        {
            Program.ShowProgressDel del = new Program.ShowProgressDel(Program.ShowProgress);
            
            string logfilePath = String.Empty;

            string nameOfFile = startOfExecuting.ToString();
            nameOfFile = nameOfFile.Replace(':', '_'); // to solve incorrect name of file. Windows exception.
            logfilePath = Path.Combine(Directory.GetCurrentDirectory(), nameOfFile + ".txt");

            Thread myThread = new Thread(()=>
           // Task myThread = Task.Factory.StartNew(()=>
            {
                
               
                Thread.Sleep(2222);
                if (File.Exists(logfilePath))
                {
                    File.AppendAllText(logfilePath, message + Environment.NewLine);
                }
                else
                {
                    using (FileStream fs = File.Create(logfilePath))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(message + Environment.NewLine);
                        fs.Write(info, 0, info.Length);
                    }
                }
                

            });
             myThread.Start();

            //while (myThread.IsAlive)
            //{
            //    del.Invoke();
            //};

        }

        //print into console, write to file
        public  void DisplayProcess(string name)
        {
            message = string.Format("{0}: {1}",name, GetSquare());
            Console.WriteLine(message);
            WriteToFile(message);
        }
    }

    public class Triangle : Figure
    {
        public double a;
        public double b;
        public double c;


        public Triangle(double[] param, string name)
        {
            try
            {
                this.a = param[0];
                this.b = param[1];
                this.c = param[2];


                Console.ForegroundColor = ConsoleColor.Yellow;

                //message = string.Format("Tringle: {0}", GetSquare());
                //Console.WriteLine(message);
                //WriteToFile(message);
                DisplayProcess(name);

            }

            // in case when we do not have all 3 sizes
            catch
            {
                return;
            }


        }

        public override double GetSquare()
        {
            double p = (a + b + c) / 2;
            double square = Math.Round(Math.Sqrt(p * (p - a) * (p - b) * (p - c)), 5);

            if (square.ToString() == "NaN")
            {
                // in case when we incorrect sizes
                throw new Exception();
            }

            return square;
        }
    }

    public class Square : Figure
    {
        public double a;
        public Square(double[] param, string name)
        {
            this.a = param[0];
            Console.ForegroundColor = ConsoleColor.Green;

            DisplayProcess(name);
        }

        public override double GetSquare()
        {
            return Math.Pow(a, 2);
        }

    }

    public class Circle : Figure
    {
        public double radius;
        public Circle(double[] param, string name)
        {
            this.radius = param[0];
            Console.ForegroundColor = ConsoleColor.Red;

            DisplayProcess(name);
        }

        public override double GetSquare()
        {
            return Math.Round(Math.PI * Math.Pow(radius, 2), 5);
        }
    }

    public class Ellipse : Figure
    {
        public double a;
        public double b;

        public Ellipse(double[] param, string name)
        {
            try
            {
                this.a = param[0];
                this.b = param[1];
                Console.ForegroundColor = ConsoleColor.White;

                DisplayProcess(name);
            }
            // in case when we do not have all 2 sizes
            catch
            {
                return;
            }
            
        }

        public override double GetSquare()
        {
            return Math.Round(Math.PI * a * b, 5);
        }
    }

    public class Rectangle : Figure
    {
        public double a;
        public double b;

        public Rectangle(double[] param, string name)
        {
            try
            {
                this.a = param[0];
                this.b = param[1];
                Console.ForegroundColor = ConsoleColor.Blue;

                DisplayProcess(name);
            }
            catch
            {
                return;
            }
            
        }
        public override double GetSquare()
        {
            return a * b;
        }
    }

    
}
