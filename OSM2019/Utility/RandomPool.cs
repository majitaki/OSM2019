using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    class RandomPool
    {
        static Dictionary<string, InitableRandom> dict = new Dictionary<string, InitableRandom>();

        public static void Declare(SeedEnum seed_enum, int seed)
        {
            InitableRandom rand = new InitableRandom(seed);
            dict[seed_enum.ToString()] = rand; //
        }

        public static InitableRandom Get(SeedEnum seed_enum)
        {
            if (!dict.ContainsKey(seed_enum.ToString()))
            {
                throw new Exception(seed_enum.ToString() + "is not declared");
            }

            return dict[seed_enum.ToString()];
        }
    }

    class InitableRandom
    {
        int seed = 0;

        Random rand;

        public InitableRandom(int s)
        {
            seed = s;
            rand = new Random(seed);
        }

        public virtual int Seed
        {
            get
            {

                return seed;
            }
            set
            {
                seed = value;
            }
        }

        public virtual void Init()
        {
            rand = new Random(seed);
        }

        public virtual int Next()
        {
            return rand.Next();
        }

        public virtual int Next(int max)
        {
            return rand.Next(max);
        }

        public virtual int Next(int min, int max)
        {
            return rand.Next(min, max);
        }

        public virtual void NextBytes(byte[] buffer)
        {
            rand.NextBytes(buffer);
        }

        public virtual double NextDouble()
        {
            return rand.NextDouble();
        }

        public virtual double NextDouble(double min, double max)
        {
            return (max - min) * rand.NextDouble() + min;
        }

        // 平均mu, 標準偏差sigmaの正規分布乱数を得る。Box-Muller法による。
        public virtual double NextNormal(double mu, double sigma)
        {
            if (Flag)
            {
                Alpha = rand.NextDouble();
                Beta = rand.NextDouble() * Math.PI * 2;
                BoxMuller1 = Math.Sqrt(-2 * Math.Log(Alpha));
                BoxMuller2 = Math.Sin(Beta);
            }
            else
            {
                BoxMuller2 = Math.Cos(Beta);
            }

            Flag = !Flag;
            return sigma * (BoxMuller1 * BoxMuller2) + mu;
        }
        private double Alpha, Beta, BoxMuller1, BoxMuller2;
        private bool Flag = false;


        public virtual List<int> getRandomIndexes(int from, int to)
        {
            if (!(from >= to))
            {
                throw new Exception("parameters should be: from >= to");
            }

            List<int> indexes = new List<int>();
            for (int i = 0; i < from; i++)
            {
                indexes.Add(i);
            }


            List<int> selectedIndexes = new List<int>();

            for (int i = 0; i < to; i++)
            {
                int r = Next(indexes.Count);
                {
                    selectedIndexes.Add(indexes.ElementAt(r));
                    indexes.RemoveAt(r);
                }
            }

            return selectedIndexes;
        }
    }
}
