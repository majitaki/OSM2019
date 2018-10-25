using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
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

        MersenneTwister rand;

        public InitableRandom(int seed)
        {
            rand = new MersenneTwister(seed);
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
            rand = new MersenneTwister(seed);
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

        public virtual double NextDouble()
        {
            return rand.NextDouble();
        }

        public virtual double NextDouble(double min, double max)
        {
            return (max - min) * rand.NextDouble() + min;
        }

        public virtual double NextNormal(double mu, double stddev)
        {
            return Normal.Sample(rand, mu, stddev);
        }

        public virtual List<double> NextNormals(double mu, double stddev, int size, double bound_rate)
        {
            double sample;
            List<double> sample_list = new List<double>();

            foreach (var count in Enumerable.Range(1, size))
            {
                do
                {
                    sample = Normal.Sample(rand, mu, stddev);
                } while (!(sample > mu - mu * bound_rate && sample < mu + mu * bound_rate));
                sample_list.Add(sample);
            }

            return sample_list;
        }


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
