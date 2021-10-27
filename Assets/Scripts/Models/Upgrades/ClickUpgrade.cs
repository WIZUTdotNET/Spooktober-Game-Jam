using System.Numerics;

namespace Models.Upgrades
{
    public class ClickUpgrade : Upgrade
    {
        public ClickUpgrade(string name, BigInteger price, UpgradeType upgradeType, double value)
        {
            Name = name;
            Price = price;
            UpgradeType = upgradeType;
            Value = value;
        }
    }
}