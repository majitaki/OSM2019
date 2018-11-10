using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.OSM
{
    class RandomNumberManager
    {
        Dictionary<SeedEnum, ExtendRandom> RandomDictionary;

        public RandomNumberManager()
        {
            this.RandomDictionary = new Dictionary<SeedEnum, ExtendRandom>();
        }

        public void Register(SeedEnum seed_enum, int seed)
        {
            ExtendRandom extended_random = new ExtendRandom(seed_enum, seed);
            this.RandomDictionary[seed_enum] = extended_random;
        }

        public ExtendRandom Get(SeedEnum seed_enum)
        {
            if (!this.RandomDictionary.ContainsKey(seed_enum))
            {
                throw new Exception(seed_enum.ToString() + " is not registered");
            }
            return this.RandomDictionary[seed_enum];
        }

        public void SetAgentGenerateRand(int seed)
        {
            this.Register(SeedEnum.AgentGenerateSeed, seed);
        }

        public ExtendRandom GetAgentGenerateRand()
        {
            return this.Get(SeedEnum.AgentGenerateSeed);
        }
    }

    class ExtendRandom
    {
        public int Seed { get; }
        public SeedEnum MySeedEnum { get; }
        MersenneTwister rand;

        public ExtendRandom(SeedEnum seed_enum, int seed)
        {
            this.MySeedEnum = seed_enum;
            this.Seed = seed;
            rand = new MersenneTwister(this.Seed);
        }

        public ExtendRandom(int seed)
        {
            this.Seed = seed;
            rand = new MersenneTwister(this.Seed);
        }

        public void Initialize()
        {
            rand = new MersenneTwister(this.Seed);
        }

        public int Next()
        {
            return rand.Next();
        }

        public int Next(int max)
        {
            return rand.Next(max);
        }

        public int Next(int min, int max)
        {
            return rand.Next(min, max);
        }

        public double NextDouble()
        {
            return rand.NextDouble();
        }

        public double NextDouble(double min, double max)
        {
            return (max - min) * rand.NextDouble() + min;
        }

        public double NextNormal(double mean, double stddev)
        {
            return Normal.Sample(rand, mean, stddev);
        }

        public virtual List<double> NextNormals(double mean, double stddev, int size, double bound_rate)
        {
            double sample;
            List<double> sample_list = new List<double>();

            foreach (var count in Enumerable.Range(1, size))
            {
                do
                {
                    sample = Normal.Sample(rand, mean, stddev);
                } while (!(sample > mean - mean * bound_rate && sample < mean + mean * bound_rate));
                sample_list.Add(sample);
            }

            return sample_list;
        }

    }
}
