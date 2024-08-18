using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateChanger<T> : MonoBehaviour
{
    public abstract T State { get; }
    public abstract event EventHandler<T> OnStateSwitch;
}

public class ScaleTimer : StateChanger<bool>
{
    bool state;
    public override bool State { get => state; }
    public override event EventHandler<bool> OnStateSwitch;
    Scalable scalable;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        scalable = GetComponent<Scalable>();
        state = false;
        renderer = GetComponent<Renderer>();
        renderer.material.SetFloat("_State", 0f);
        scalable.OnTransitionEnd += (object sender, ScaleState scaleState) =>
        {
            state = !state;
            renderer.material.SetFloat("_State", state ? 1f : 0f);
            if (OnStateSwitch != null) OnStateSwitch(this, state);
            scalable.Toggle();
        };
        StartCoroutine(StartToggle());
    }

    IEnumerator StartToggle()
    {
        yield return null;
        scalable.Toggle();
    }
}
