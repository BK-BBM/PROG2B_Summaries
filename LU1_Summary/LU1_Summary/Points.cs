using System;
using System.Collections.Generic;
using System.Text;

namespace LU1_Summary
{
    internal class Points
    {
        public int X; //(1,
        public int Y;//(,3)
        public Points(int x, int y)
        {
            X = x;
            Y = y;
        }

        //overload the + operator


        //a method that overloads an operator must always be public static
        //and the operator being overaloaded needs to be preceded by the 'operator'
        //keyword.
        public static Points operator +(Points pointsA, Points pointsB)
        {

            return new Points(pointsA.X + pointsB.X, pointsA.Y + pointsB.Y);
        }
        public void display()
        {
            
            Console.WriteLine("The values of X & Y added!");
            Console.WriteLine($"{X},{Y}");
        }
    }
}
