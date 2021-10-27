using System.Numerics;

namespace Models.Upgrades
{
    public class GeneratorUpgrade : Upgrade
    {
        public GeneratorUpgrade(string name, BigInteger price, UpgradeType upgradeType, double value, Generator targetGenerator)
        {
            Name = name;
            Price = price;
            UpgradeType = upgradeType;
            Value = value;
            TargetGenerator = targetGenerator;
        }
    }
}