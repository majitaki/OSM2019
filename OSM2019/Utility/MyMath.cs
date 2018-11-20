using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    class MyMath
    {
        public static double Ceiling(double d, int dn)
        {
            int p = MyMath.Pow(10, dn);
            d = d * p;

            d = Math.Ceiling(d);

            return (d / p);
        }

        public static int Pow(int x, int y)
        {
            try
            {
                var r = 1;
                for (int i = 0; i < y; i++)
                {
                    r *= x;
                }
                return r;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}