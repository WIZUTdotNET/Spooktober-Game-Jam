using Extensions;
using Models.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UpgradeManager : MonoBehaviour
    {
        public Upgrade Upgrade { get; private set; }
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Price;
        private GameManager _gameManager;

        public void Initialize(Upgrade upgrade)
        {
            _gameManager = FindObjectOfType<GameManager>();
            Upgrade = upgrade;
            GameManager.OnUpgradeBought += GameManager_OnUpgradeBought; ;
            GameManager.OnTick += GameManager_OnTick;
            GetComponent<Button>().onClick.AddListener(Buy);

            Name.GetComponent<TextMeshProUGUI>().text = $"{Upgrade.Name}";
            Description.GetComponent<TextMeshProUGUI>().text = $"Placeholder";
            Price.GetComponent<TextMeshProUGUI>().text = $"{Upgrade.Price.ToHumanString()}";
        }

        private void GameManager_OnUpgradeBought(object sender, Events.OnUpgradeBoughtEventArgs e)
        {
            if (e.Upgrade != Upgrade)
                return;

            Destroy(this.transform.gameObject);
        }

        private void GameManager_OnTick(object sender, Events.OnTickEventArgs e)
        {
            //TOOD: Block upgrade visually if not enough money
            if (_gameManager.CanBuyUpgrade(Upgrade))
            {

            }
        }
        private void Buy()
        {
            Debug.Log("Kupujem upgrade");
            if (_gameManager.CanBuyUpgrade(Upgrade))
            {
                _gameManager.BuyUpgrade(Upgrade);
            }
        }
    }
}