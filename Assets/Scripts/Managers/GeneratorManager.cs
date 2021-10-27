using Extensions;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GeneratorManager : MonoBehaviour
    {
        public Generator Generator { get; private set; }
        public TextMeshProUGUI Name;
        public TextMeshProUGUI SoulsPerSecond;
        public TextMeshProUGUI Price;
        private GameManager _gameManager;

        public void Initialize(Generator generator)
        {
            _gameManager = FindObjectOfType<GameManager>();
            Generator = generator;
            GameManager.OnGeneratorBought += GameManager_OnGeneratorBought;
            GameManager.OnTick += GameManager_OnTick;
            GameManager.OnUpgradeBought += GameManager_OnUpgradeBought;
            GetComponent<Button>().onClick.AddListener(Buy);
        }

        private void Buy()
        {
            Debug.Log("Kupujem");
            if (_gameManager.CanBuyGenerators(Generator, 1))
            {
                _gameManager.BuyGenerators(Generator, 1);
            }
        }

        private void UpdateText()
        {
            Name.GetComponent<TextMeshProUGUI>().text = $"{Generator.Name} ({Generator.Count})";
            SoulsPerSecond.GetComponent<TextMeshProUGUI>().text = $"{_gameManager.GeneratorSoulsPerSecond[Generator].ToHumanString()}"; 
            Price.GetComponent<TextMeshProUGUI>().text = $"{Generator.GetCurrentPriceForMultiple(1).ToHumanString()}";
        }

        private void GameManager_OnTick(object sender, Events.OnTickEventArgs e)
        {
            //TODO: Implement check for buyable.
            if (_gameManager.CanBuyGenerators(Generator, 1))
            {

            }
        }

        private void GameManager_OnUpgradeBought(object sender, Events.OnUpgradeBoughtEventArgs e)
        {
            UpdateText();
        }

        private void GameManager_OnGeneratorBought(object sender, Events.OnGeneratorBoughtEventArgs e)
        {
            
            if (e.Generator == Generator)
            {
                UpdateText();
            }
        }
    }
}