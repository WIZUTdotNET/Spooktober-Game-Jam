using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int soulOnClick = 1;
    public int soulsPerSecond = 0;
    private int _souls;
    private TextMeshProUGUI _soulsText;
    private EventHandler<TimeTickSystem.OnTickEvents> _tickSystemDelegate;

    
    private void Start()
    {
        _soulsText = GameObject.Find("Canvas/Souls").GetComponent<TextMeshProUGUI>();
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
    }

    public void AddSouls()
    {
        _souls += soulOnClick;
        UpdateSoulsAmount();
    }
}
