using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSwitch : StateChanger<bool>
{
    int collidersInside = 0;
    bool _state;
    
    public override bool State { get => _state; }
    public override event EventHandler<bool> OnStateSwitch;
    Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_TriggerTime", -100);
        SetState(collidersInside > 0);
        OnStateSwitch += (object sender, bool currentState) =>
        {
            if (currentState)
            {
                renderer.material.SetFloat("_TriggerTime", Time.timeSinceLevelLoad);
            }
        };
    }

    void SetState(bool newState)
    {
        bool change = _state != newState;
        _state = newState;
        renderer.material.SetFloat("_State", _state ? 1f : 0f);
        if (change)
        {
            OnStateSwitch?.Invoke(this, _state);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        collidersInside++;
        SetState(collidersInside > 0);
    }

    private void OnTriggerExit(Collider other)
    {
        collidersInside--;
        SetState(collidersInside > 0);
    }
}
