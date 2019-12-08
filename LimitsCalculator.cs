using System;
using System.Collections.Generic;
using System.Text;

namespace LimitsDBGenerator
{
    public static class LimitCalculator
    {
        public static string CalculateLimit(Polynom f, Polynom g, string value)
        {
            string ans = "";
            double doubleValue;
            //if (value == "Infinity")
            //{
            //    if (f.Coefs.Count > g.Coefs.Count)
            //    {
            //        ans = "Infinity";
            //        return ans;
            //    }
            //    else if (f.Coefs.Count < g.Coefs.Count)
            //    {
            //        ans = "0";
            //        return ans;
            //    }
            //    else
            //    {
            //        ans = (f.Coefs[f.Coefs.Count - 1] / g.Coefs[f.Coefs.Count - 1]).ToString();
            //        return ans;
            //    }
            //}
            double.TryParse(value, out doubleValue);
            try
            {
                if (!(f.ValueAt(doubleValue) == 0 && g.ValueAt(doubleValue) == 0))
                {
                    ans = (f.ValueAt(doubleValue) / g.ValueAt(doubleValue)).ToString();
                    return ans;
                }
                while (f.ValueAt(doubleValue) == 0 && g.ValueAt(doubleValue) == 0)
                {
                    f = Derivative(f);
                    g = Derivative(g);
                }
                if (g.ValueAt(doubleValue) != 0)
                    ans = (f.ValueAt(doubleValue) / g.ValueAt(doubleValue)).ToString();
                else
                    ans = "?";//symbol for limit doesn't exist
            }
            catch
            {
                Console.WriteLine("too large...");
            }
            return ans;
        }

        public static Polynom Derivative(Polynom f)
        {
            var dF = new Polynom();
            for (int i = 0; i < f.Coefs.Count - 1; ++i)
                dF.Coefs.Add(f.Coefs[i + 1] * (i + 1));
            return dF;
        }
    }
}
