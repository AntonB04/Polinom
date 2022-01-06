using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polynom
{
    class Polynom
    {
        public int[] Coefficients { get; protected set; }
        protected int numberOfMembers; 
        public Polynom(int[] coefficients)
        {
            Coefficients = coefficients;
            numberOfMembers = coefficients.Length;
        }

        public int GetCoef(int degree)
        {
            if (degree < numberOfMembers)
            {
                return Coefficients[degree];
            }
            else
            {
                return 0;
            }
        }

        public static Polynom operator +(Polynom polynom1, Polynom polynom2)
        {
            var max = new[] { polynom1.Coefficients.Length, polynom2.Coefficients.Length }.Max();
            
            int[] sumCoef = new int[max];

            for (int i = 0; i < max; i++)
            {
                sumCoef[i] = polynom1.GetCoef(i) + polynom2.GetCoef(i);
            }

            return new Polynom(sumCoef);
        }

        public static Polynom operator -(Polynom polynom1, Polynom polynom2)
        {
            var max = new[] { polynom1.Coefficients.Length, polynom2.Coefficients.Length }.Max();
            
            int[] difCoef = new int[max];

            for (int i = 0; i < max; i++)
            {
                difCoef[i] = polynom1.GetCoef(i) - polynom2.GetCoef(i);
            }

            return new Polynom(difCoef);
        }

        public static Polynom operator *(Polynom polynom1, Polynom polynom2)
        {
            var rank = polynom1.Coefficients.Length * polynom2.Coefficients.Length;

            int[] polynomsMap = new int[rank];

            for (int i = 0; i < polynom1.Coefficients.Length; i++)
            {
                int degree;
                
                for (int j = 0; j < polynom2.Coefficients.Length; j++)
                {
                    degree = i + j;
                    polynomsMap[degree] += polynom1.GetCoef(i) * polynom2.GetCoef(j);
                }

            }
            
            return new Polynom(polynomsMap);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < Coefficients.Length; i++)
            {
                
                if (i == 0)
                {
                    result.Append(Coefficients[i].ToString());
                }
                else if(Coefficients[i] > 0)
                {
                    StringBuilder monomial = new StringBuilder(Coefficients[i].ToString() + "x^" + i.ToString());
                    result.Append(" + " + monomial);
                }
                else if(Coefficients[i] < 0)
                {
                    StringBuilder monomial = new StringBuilder(Math.Abs(Coefficients[i]).ToString() + "x^" + i.ToString());
                    result.Append(" - " + monomial);
                }
            }

            return result.ToString();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            // -3 + 4x^2 + 5x^5 - 12x^7
            // 4 - 1x^2 + 6x^3 - 5x^5 + 5x^7
            // sum:
            // 1 + 3x^2 + 6x^3 - 7x^7
            var polynom1 = new Polynom(new [] {-3, 0, 4, 0, 0, 5, 0, -12});
            var polynom2 = new Polynom(new[] { 4, 0, -1, 6, 0, -5, 0, 5 });
            var sum = polynom1 + polynom2;
            Console.WriteLine(sum);

            // 7 + 3x^2 + 2x^5 + 4x^7
            // 4 + 3x^2 + 6x^3 - 5x^5 - 1x^7
            // dif:
            // 3 - 6x^3 + 7x^5 + 5x^7
            var polynom3 = new Polynom(new[] { 7, 0, 3, 0, 0, 2, 0, 4 });
            var polynom4 = new Polynom(new[] { 4, 0, 3, 6, 0, -5, 0, -1 });
            var dif = polynom3 - polynom4;
            Console.WriteLine(dif);

            // -1 + x^2
            // 1 + 6x^2 + 9x^4
            // mult:
            // -1 - 6x^2 - 9x^4 + x^2 + 6x^4 + 9x^6 = -1 -5x^2 - 3x^4 + 9x^6
            var polynom5 = new Polynom(new[] { -1, 0, 1 });
            var polynom6 = new Polynom(new[] { 1, 0, 6, 0, 9 });
            var mult = polynom5 * polynom6;
            Console.WriteLine(mult);
        }
    }
}
