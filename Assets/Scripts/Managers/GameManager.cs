using Events;
using Managers;
using Models;
using Models.Upgrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //public int soulsOnClick = 1;
        //public int soulsPerSecond = 0;
    
        //public int soulsOnClickPrice = 10;
        //public int soulsPerSecondPrice = 50;
    
        //private int _souls;
    
        //private TextMeshProUGUI _soulsText;
        //private TextMeshProUGUI _soulsOnClickText;
        //private TextMeshProUGUI _soulsPerSecondText;
    
        //private TextMeshProUGUI _soulsOnClickPriceText;
        //private TextMeshProUGUI _soulsPerSecondPriceText;
    
        //private EventHandler<TimeTickSystem.OnTickEvents> _tickSystemDelegate;

        //private void Start()
        //{
        //    _soulsText = GameObject.Find("Canvas/Souls").GetComponent<TextMeshProUGUI>();
        
        //    _soulsOnClickText = GameObject.Find("Canvas/Stats/Clicks/SPC").GetComponent<TextMeshProUGUI>();
        //    _soulsPerSecondText = GameObject.Find("Canvas/Stats/Ticks/SPT").GetComponent<TextMeshProUGUI>();
        
        //    _soulsOnClickPriceText = GameObject.Find("UI/Click/Price").GetComponent<TextMeshProUGUI>();
        //    _soulsPerSecondPriceText = GameObject.Find("UI/Tick/Price").GetComponent<TextMeshProUGUI>();
        //    _tickSystemDelegate = delegate
        //    {
        //        _souls += soulsPerSecond;
        //        UpdateSoulsAmount();
        //    };
        //    TimeTickSystem.OnTick += _tickSystemDelegate;
        //}

        //private void UpdateSoulsAmount()
        //{
        //    _soulsText.text = _souls.ToString();
        
        //    _soulsOnClickText.text = soulsOnClick.ToString();
        //    _soulsPerSecondText.text = soulsPerSecond.ToString();
        
        //    _soulsOnClickPriceText.text = soulsOnClickPrice.ToString();
        //    _soulsPerSecondPriceText.text = soulsPerSecondPrice.ToString();
        //}

        public static event EventHandler<OnUpgradeBoughtEventArgs> OnUpgradeBought;
        public static event EventHandler<OnGeneratorBoughtEventArgs> OnGeneratorBought;
        public static event EventHandler<OnClickEventArgs> OnClick;
        public static event EventHandler<OnTickEventArgs> OnTick;
        public BigInteger SoulsPerClick { get; private set; } = new BigInteger(1000);
        public BigInteger SoulsPerSecond { get; private set; } = BigInteger.Zero;
        public IDictionary<Generator, BigInteger> GeneratorSoulsPerSecond { get; private set; }
            = new Dictionary<Generator, BigInteger>();
        public BigInteger Souls { get; private set; } = BigInteger.Zero;
        public IEnumerable<Upgrade> Upgrades { get; set; } = new List<Upgrade>();
        public IEnumerable<Generator> Generators { get; set; } = new List<Generator>();
        
        // I don't know where to put it so for now it will be here (by aweczet)
        [SerializeField] private GameObject soulPrefab;

        private void Awake()
        {
            //TODO: Fetch all upgrades and save them in list
            //TODO: Fetch all generators and save them in list
        }

        private void Start()
        {
            TimeTickSystem.OnTick += TickHandler;
            Generators = FindObjectOfType<GeneratorsManager>().Generators;
            Upgrades = FindObjectOfType<GeneratorsManager>().Upgrades;
        }

        public void Initialize(BigInteger souls, IEnumerable<Generator> generators, IEnumerable<Upgrade> upgrades)
        {
            throw new NotImplementedException("TODO: Implement deserialization/save system");
        }

        bool _firstFramePassed = false;
        void Update()
        {
            // Debug.Log(Souls);
            if (_firstFramePassed)
                return;
            _firstFramePassed = true;
            RecalculateSoulsPerClick();
            RecalculateSoulsPerSecond();
            foreach (var generator in Generators)
            {
                OnGeneratorBought?.Invoke(this, new OnGeneratorBoughtEventArgs(generator));
            }
        }
        private BigInteger CalculateIncomeWithUpgrades(BigInteger income, IEnumerable<Upgrade> upgrades)
        {
            var multiplicativeUpgrades = 1.0;
            var additiveUpgrades = 0.0;
            var flatUpgrades = income;
            foreach (var upgrade in upgrades)
            {
                switch (upgrade.UpgradeType)
                {
                    case UpgradeType.MultiplicativeUpgrade:
                        multiplicativeUpgrades *= upgrade.Value;
                        break;
                    case UpgradeType.AdditiveUpgrade:
                        additiveUpgrades += upgrade.Value;
                        break;
                    case UpgradeType.FlatUpgrade:
                        flatUpgrades += new BigInteger(upgrade.Value * 1000);
                        break;
                }
            }
            return
                (flatUpgrades * (long)(additiveUpgrades * 1000)) / 1000 +
                (flatUpgrades * (long)(multiplicativeUpgrades * 1000)) / 1000;
        }

        private void RecalculateSoulsPerSecond()
        {
            var generatorsUpgrades = Generators
                .GroupJoin(
                    Upgrades.Where(u => u.Bought),
                    generator => generator,
                    upgrade => upgrade.TargetGenerator,
                    (generator, upgrades) => new
                    {
                        Generator = generator,
                        Upgrades = upgrades,
                    }
                )
                .DefaultIfEmpty();
            SoulsPerSecond = BigInteger.Zero;
            foreach (var generatorUpgrades in generatorsUpgrades)
            {
                var generator = generatorUpgrades.Generator;
                var baseSoulsProduction = generator.GetProduction();
                var income = CalculateIncomeWithUpgrades(generator.GetProduction(), generatorUpgrades.Upgrades);
                GeneratorSoulsPerSecond[generator] = income;
                SoulsPerSecond += income;
            }
        }

        private void RecalculateSoulsPerClick()
        {
            var clickUpgrades = Upgrades.Where(u => u is ClickUpgrade && u.Bought).ToList();
            SoulsPerClick = CalculateIncomeWithUpgrades(new BigInteger(1000), clickUpgrades);
        }

        public bool CanBuyUpgrade(Upgrade upgrade)
            => upgrade.Price <= Souls;

        public void BuyUpgrade(Upgrade upgrade)
        {
            Souls -= upgrade.Price;
            upgrade.Buy();
            RecalculateSoulsPerClick();
            RecalculateSoulsPerSecond();
            OnUpgradeBought?.Invoke(this, new OnUpgradeBoughtEventArgs(upgrade));
        }

        public bool CanBuyGenerators(Generator generator, int count)
            => generator.GetCurrentPriceForMultiple(count) <= Souls;

        public void BuyGenerators(Generator generator, int count)
        {
            Souls -= generator.GetCurrentPriceForMultiple(count);
            generator.Buy(count);
            RecalculateSoulsPerClick();
            RecalculateSoulsPerSecond();
            OnGeneratorBought?.Invoke(this, new OnGeneratorBoughtEventArgs(generator));
        }

        public void Click()
        {
            Souls += SoulsPerClick;
            Instantiate(soulPrefab);
            OnClick?.Invoke(this, new OnClickEventArgs(Souls));
        }

        public void ClickX1000()
        {
            Souls += SoulsPerClick * 1000;
            OnClick?.Invoke(this, new OnClickEventArgs(Souls));
        }

        public void ClickX100000()
        {
            Souls += SoulsPerClick * 100000;
            OnClick?.Invoke(this, new OnClickEventArgs(Souls));
        }

        public void TickHandler(object sender, TimeTickSystem.OnTickEvents e)
        {
            Souls += SoulsPerSecond;
            OnTick?.Invoke(this, new OnTickEventArgs(Souls));
        }
    }
}