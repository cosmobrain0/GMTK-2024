using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : StateChanger<bool>
{
    bool _switchState;
    public override bool State { get => _switchState; }
    public override event EventHandler<bool> OnStateSwitch;
    Scalable scalable;
    public Renderer switchSensorRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _switchState = false;
        scalable = GetComponent<Scalable>();
        scalable.OnTransitionEnd += (object sender, ScaleState endState) =>
        {
            if (endState == ScaleState.Large)
            {
                if (OnStateSwitch != null) OnStateSwitch(this, true);
            }
        };
        scalable.OnTransitionStart += (object sender, ScaleState targetState) =>
        {
            if (_switchState)
            {
                if (OnStateSwitch != null) OnStateSwitch(this, false);
            }
        };

        OnStateSwitch += (object sender, bool currentState) =>
        {
            _switchState = currentState;
        };
        OnStateSwitch += (object sender, bool currentState) =>
        {
            if (currentState)
            {
                switchSensorRenderer.material.color = Color.yellow;
            }
            else switchSensorRenderer.material.color = Color.white;
        };
    }
}
