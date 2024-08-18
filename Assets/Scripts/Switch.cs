using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    bool _switchState;
    public bool State { get => _switchState; }
    public event EventHandler<bool> OnStateToggle;
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
                if (OnStateToggle != null) OnStateToggle(this, true);
            }
        };
        scalable.OnTransitionStart += (object sender, ScaleState targetState) =>
        {
            if (_switchState)
            {
                if (OnStateToggle != null) OnStateToggle(this, false);
            }
        };

        OnStateToggle += (object sender, bool currentState) =>
        {
            _switchState = currentState;
        };
        OnStateToggle += (object sender, bool currentState) =>
        {
            if (currentState)
            {
                switchSensorRenderer.material.color = Color.yellow;
            }
            else switchSensorRenderer.material.color = Color.white;
        };
    }
}
