using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaTimer : StateChanger<bool>
{
    public float timerDuration;
    DateTime? timerStart;
    bool isEmpty;
    int collidersInside = 0;
    Renderer renderer;

    public override bool State { get => timerStart != null; }
    public override event EventHandler<bool> OnStateSwitch;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        timerStart = null;
        renderer.material.SetFloat("_TriggerDuration", timerDuration / 1000f);
        renderer.material.SetFloat("_TriggerTime", -100);
        OnStateSwitch += (object sender, bool currentState) =>
        {
            if (currentState) renderer.material.SetFloat("_TriggerTime", Time.timeSinceLevelLoad);
        };
    }

    private void Update()
    {
        if (collidersInside > 0)
        {
            bool justSwitched = timerStart == null;
            timerStart = DateTime.Now;
            if (justSwitched) OnStateSwitch?.Invoke(this, true);
        }
        if (timerStart != null && (DateTime.Now-timerStart)?.TotalMilliseconds > timerDuration)
        {
            timerStart = null;
            OnStateSwitch?.Invoke(this, false);
        }
        renderer.material.SetFloat("_Progress", timerStart == null ? -1f : (float)((DateTime.Now - timerStart)?.TotalMilliseconds) / timerDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        collidersInside++;
    }

    private void OnTriggerExit(Collider other)
    {
        collidersInside--;
    }
}
