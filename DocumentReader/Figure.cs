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
       
        public string message; // display message into console
        public abstract double GetSquare();

        Program.WriteToFileDel writeDel = new Program.WriteToFileDel(Program.EstendInfo); 


        // write into file from another thread via delegate
        public void  WriteToFile(string message)
        {
            
            Thread myThread = new Thread(()=>
            {
               
                writeDel(message);
                
            });
             myThread.Start();

             
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
            try
            {
                this.a = param[0];
                Console.ForegroundColor = ConsoleColor.Green;

                DisplayProcess(name);
            }
            catch
            {
                return;
            }
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
            try
            {
                this.radius = param[0];
                Console.ForegroundColor = ConsoleColor.Red;

                DisplayProcess(name);
            }
            catch
            {
                return;
            }
            
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
