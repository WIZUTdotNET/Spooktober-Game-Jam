using System;
using System.Numerics;

namespace Models
{
    [System.Serializable]
    public class Generator
    {
        public BigInteger StartingPrice { get; private set; }
        public int Count { get; private set; }
        public string Name { get; private set; }
        public double PriceRateGrowth { get; private set; }
        private static int Precision = (int)Math.Pow(10, 3);
        private BigInteger _baseProduction;

        public Generator(BigInteger startingPrice, BigInteger baseProduction, string name, double priceRateGrowth)
        {
            StartingPrice = startingPrice;
            _baseProduction = baseProduction;
            Name = name;
            PriceRateGrowth = priceRateGrowth;
        }

        public void Buy(int count)
        {
            Count += count;
        }

        public BigInteger GetCurrentPriceForMultiple(int count)
        {
            //TODO: Replace with geometric mean
            var sum = new BigInteger();
            for (int n = Count; n < Count + count; n++)
            {
                sum += GetPriceForNth(n + 1);
            }
            return sum;
        }

        public BigInteger GetPriceForNth(int n)
        {
            var priceMultiplier = Math.Pow((double)n, PriceRateGrowth) * Precision;
            return StartingPrice * (long)priceMultiplier / Precision;
        }

        public BigInteger GetProduction()
        {
            return Count * _baseProduction;
        }
    }
}