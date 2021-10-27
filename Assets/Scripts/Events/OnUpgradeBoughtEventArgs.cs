using System;
using Models.Upgrades;

namespace Events
{
    public class OnUpgradeBoughtEventArgs : EventArgs
    {
        public Upgrade Upgrade { get; private set; }

        public OnUpgradeBoughtEventArgs(Upgrade upgrade)
        {
            Upgrade = upgrade;
        }
    }
}