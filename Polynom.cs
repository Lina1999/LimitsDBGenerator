using System;
using System.Collections.Generic;
using System.Text;

namespace LimitsDBGenerator
{
    public class Polynom
    {
        public List<double> Coefs { get; set; } = new List<double>();

        public decimal ValueAt(double x)
        {
            decimal val = 0;
            for (int i = 0; i < Coefs.Count; ++i)
                val += (decimal)(Coefs[i] * Math.Pow(x, i));
            return val;
        }

        public void Print()
        {
            for (int i = 0; i < Coefs.Count; ++i)
            {
                Console.Write("{0}x ^ {1}", Coefs[i], i);
                if (i != Coefs.Count - 1)
                    Console.Write(" + ");
            }
        }
    }
}
