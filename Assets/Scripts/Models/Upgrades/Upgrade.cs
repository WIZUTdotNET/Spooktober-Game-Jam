using Exceptions.Upgrades;
using System.Numerics;

namespace Models.Upgrades
{
    public abstract class Upgrade
    {
        public string Name { get; protected set; }
        public BigInteger Price { get; protected set; }
        public UpgradeType UpgradeType { get; protected set; }
        public double Value { get; protected set; }
        public Generator TargetGenerator { get; protected set; }
        public bool Bought { get; protected set; } = false;
        public void Buy()
        {
            if (Bought)
                throw new UpgradeAlreadyBoughtException($"Upgrade {Name} for price {Price} was already bought before.");
            Bought = true;
        }
    }
}