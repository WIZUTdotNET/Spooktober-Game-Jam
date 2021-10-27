using System;
using UnityEngine;

public class TimeTickSystem : MonoBehaviour
{
    
    /*
     *  In other class that wants to subscribe tick event
     * 
     *  private EventHandler<TimeTickSystem.OnTickEvents> _tickSystemDelegate;
     *
     *  private void Start()
     *  {
     *      _tickSystemDelegate = delegate
     *      {
     *          // Thing to do every tick
     *      };
     *      TimeTickSystem.OnTick += _tickSystemDelegate;
     *  }
     */
    
    public class OnTickEvents : EventArgs
    {
        public int Tick;
    }

    public static event EventHandler<OnTickEvents> OnTick;

    // Number of seconds per tick
    private const float MAXTick = 1.0f;
    private int _tick;
    private float _tickTimer;

    private void Awake()
    {
        _tick = 0;
    }

    private void Update()
    {
        _tickTimer += Time.deltaTime;
        if ((_tickTimer < MAXTick))
            return;
        _tickTimer -= MAXTick;
        _tick++;
        OnTick?.Invoke(this, new OnTickEvents()
        {
            Tick = _tick
        });
    }
}