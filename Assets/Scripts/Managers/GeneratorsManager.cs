using Models;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Models.Upgrades;
using System.Linq;

namespace Managers
{
    public class GeneratorsManager : MonoBehaviour
    {
        public GameObject GeneratorManagerPrefab;
        public GameObject UpgradesWindow;
        public GameObject UpgradeGeneratorPrefab;
        public IEnumerable<Generator> Generators { get; private set; }
        public IEnumerable<Upgrade> Upgrades { get; private set; }

        void Awake()
        {
            var generators = new Dictionary<int, Generator>()
            {
                {1, new Generator(new BigInteger(10000), new BigInteger(100), "Cementary", 1.07) },
                {2, new Generator(new BigInteger(250000), new BigInteger(1999), "Aztec Altar", 1.11) },
                {3, new Generator(new BigInteger(6660000), new BigInteger(6666), "Processing Plant", 1.10) },
            };
            var upgrades = new Dictionary<int, Upgrade>()
            {
                {1, new GeneratorUpgrade("Grave Diggers I", BigInteger.Parse("100000"), UpgradeType.MultiplicativeUpgrade, 2f, generators[1]) },
                {2, new GeneratorUpgrade("Grave Diggers II", BigInteger.Parse("200000"), UpgradeType.MultiplicativeUpgrade, 2f, generators[1]) },
                {3, new GeneratorUpgrade("Grave Diggers III", BigInteger.Parse("300000"), UpgradeType.MultiplicativeUpgrade, 2f, generators[1]) },
                {4, new ClickUpgrade("Black Church Acolite I", BigInteger.Parse("100000"), UpgradeType.FlatUpgrade, 0.5)},
                {5, new ClickUpgrade("Black Church Acolite II", BigInteger.Parse("200000"), UpgradeType.FlatUpgrade, 0.5)},
                {6, new ClickUpgrade("Black Church Acolite III", BigInteger.Parse("400000"), UpgradeType.FlatUpgrade, 0.5)},
                {7, new ClickUpgrade("Acolite Training I", BigInteger.Parse("300000"), UpgradeType.MultiplicativeUpgrade, 2)},
                {8, new ClickUpgrade("Acolite Training II", BigInteger.Parse("500000"), UpgradeType.MultiplicativeUpgrade, 2)},
                {9, new ClickUpgrade("Acolite Training III", BigInteger.Parse("600000"), UpgradeType.MultiplicativeUpgrade, 2)},
            };
            Generators = generators.Values;
            Upgrades = upgrades.Values;
            int i = 0;
            foreach (var generator in generators.Values)
            {
                //TODO: Pass in icon or something like that.
                //TODO: Add description to generator.
                var manager = Instantiate<GameObject>(GeneratorManagerPrefab);
                var managerScript = manager.GetComponent<GeneratorManager>();
                managerScript.Initialize(generator);
                manager.transform.SetParent(transform);
                manager.transform.localPosition = new Vector3(0, i * -105, 0);
                i += 1;
            }
            
            i = 0;
            foreach (var upgrade in upgrades.Values.OrderBy(u => u.Price))
            {
                var update = Instantiate<GameObject>(UpgradeGeneratorPrefab);
                var managerScript = update.GetComponent<UpgradeManager>();
                managerScript.Initialize(upgrade);
                update.transform.SetParent(UpgradesWindow.transform);
                update.transform.localPosition = new Vector3((i + 1) % 2 * -165, i / 2 * -70, 0);
                i += 1;
            }

        }
    }
}
