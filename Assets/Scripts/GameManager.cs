using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int soulsOnClick = 1;
    public int soulsPerSecond = 0;
    
    public int soulsOnClickPrice = 10;
    public int soulsPerSecondPrice = 50;
    
    private int _souls;
    
    private TextMeshProUGUI _soulsText;
    private TextMeshProUGUI _soulsOnClickText;
    private TextMeshProUGUI _soulsPerSecondText;
    
    private TextMeshProUGUI _soulsOnClickPriceText;
    private TextMeshProUGUI _soulsPerSecondPriceText;
    
    private EventHandler<TimeTickSystem.OnTickEvents> _tickSystemDelegate;

    private void Start()
    {
        _soulsText = GameObject.Find("Canvas/Souls").GetComponent<TextMeshProUGUI>();
        
        _soulsOnClickText = GameObject.Find("Canvas/Stats/Clicks/SPC").GetComponent<TextMeshProUGUI>();
        _soulsPerSecondText = GameObject.Find("Canvas/Stats/Ticks/SPT").GetComponent<TextMeshProUGUI>();
        
        _soulsOnClickPriceText = GameObject.Find("UI/Click/Price").GetComponent<TextMeshProUGUI>();
        _soulsPerSecondPriceText = GameObject.Find("UI/Tick/Price").GetComponent<TextMeshProUGUI>();
        _tickSystemDelegate = delegate
        {
            _souls += soulsPerSecond;
            UpdateSoulsAmount();
        };
        TimeTickSystem.OnTick += _tickSystemDelegate;
    }

    private void UpdateSoulsAmount()
    {
        _soulsText.text = _souls.ToString();
        
        _soulsOnClickText.text = soulsOnClick.ToString();
        _soulsPerSecondText.text = soulsPerSecond.ToString();
        
        _soulsOnClickPriceText.text = soulsOnClickPrice.ToString();
        _soulsPerSecondPriceText.text = soulsPerSecondPrice.ToString();
    }

    public void AddSouls()
    {
        _souls += soulsOnClick;
        UpdateSoulsAmount();
    }

    public void BuySoulsPerClick()
    {
        if (_souls < soulsOnClickPrice) return;
        _souls -= soulsOnClickPrice;
        soulsOnClick += 1;
        soulsOnClickPrice *= 2;
        UpdateSoulsAmount();
    }

    public void BuySoulsPerTick()
    {        
        if (_souls < soulsPerSecondPrice) return;
        _souls -= soulsPerSecondPrice;
        soulsPerSecond += 1;
        soulsPerSecondPrice *= 2;
        UpdateSoulsAmount();
    }
}
