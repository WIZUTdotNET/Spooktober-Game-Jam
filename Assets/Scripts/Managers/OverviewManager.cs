using Extensions;
using Managers;
using TMPro;
using UnityEngine;
using Events;

public class OverviewManager : MonoBehaviour
{
    public TextMeshProUGUI Souls;
    public TextMeshProUGUI SoulsPerClick;
    public TextMeshProUGUI SoulsPerSecond;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        GameManager.OnTick += GameManager_OnTick;
        GameManager.OnClick += GameManager_OnClick;
        GameManager.OnGeneratorBought += GameManager_OnGeneratorBought;
        GameManager.OnUpgradeBought += GameManager_OnUpgradeBought;

    }

    private void UpdateAllText()
    {
        SoulsPerClick.GetComponent<TextMeshProUGUI>().text = $"{_gameManager.SoulsPerClick.ToHumanString()}";
        SoulsPerSecond.GetComponent<TextMeshProUGUI>().text = $"{_gameManager.SoulsPerSecond.ToHumanString()}";
        Souls.GetComponent<TextMeshProUGUI>().text = $"{_gameManager.Souls.ToHumanString()}";
    }

    private void UpdateSoulsText()
    {
        Souls.GetComponent<TextMeshProUGUI>().text = $"{_gameManager.Souls.ToHumanString()}";
    }

    private void GameManager_OnUpgradeBought(object sender, OnUpgradeBoughtEventArgs e)
        => UpdateAllText();

    private void GameManager_OnGeneratorBought(object sender, OnGeneratorBoughtEventArgs e)
        => UpdateAllText();

    private void GameManager_OnClick(object sender, OnClickEventArgs e)
        => UpdateSoulsText();

    private void GameManager_OnTick(object sender, OnTickEventArgs e)
        => UpdateSoulsText();
}
