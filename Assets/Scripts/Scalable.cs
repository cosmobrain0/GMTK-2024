#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScaleState
{
    Small,
    Large
}

public class Scalable : MonoBehaviour
{
    public float smallScale;
    public float largeScale;
    public ScaleState stateOnStart;
    public float contactForceCoefficient;

    public float scaleDuration;
    ScaleState target;
    public DateTime? transitionStart;
    public float CurrentScale { get => transform.localScale.x; set {
            transform.localScale = new Vector3(value, value, value);
            material.SetFloat("_CurrentScale", value);
        } }
    float scaleWhenStartingTransition;
    public ScaleState Target { get => target; set {
            if (value == target) return;
            target = value;
            scaleWhenStartingTransition = CurrentScale;
            transitionStart = DateTime.Now;
            if (OnTransitionStart != null) OnTransitionStart(this, target);
        } }
    float TargetScale { get => (target) switch { ScaleState.Small => smallScale, ScaleState.Large => largeScale }; }

    public event EventHandler<ScaleState> OnTransitionEnd;
    public event EventHandler<ScaleState> OnTransitionStart;

    Material material;

    // Start is called before the first frame update
    void Start()
    {
        target = stateOnStart;
        transitionStart = null;
        material = GetComponent<Renderer>().material;
        material.SetFloat("_SmallScale", smallScale);
        material.SetFloat("_LargeScale", largeScale);
        CurrentScale = TargetScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionStart == null) return;
        DateTime now = DateTime.Now;
        double? percentage = (now - transitionStart)?.TotalMilliseconds / scaleDuration;
        if (percentage == null || percentage > 1)
        {
            CurrentScale = TargetScale;
            transitionStart = null;
            if (OnTransitionEnd != null) OnTransitionEnd(this, Target);
            return;
        }
        Func<float, float> ease = x => x * x * (3f - 2f * x);
        CurrentScale = Mathf.Lerp(scaleWhenStartingTransition, TargetScale, ease((float)percentage));
    }

    float ScaleGradient(float t)
    {
        // ease is 3x^2 - 2x^3
        // d/dx of ease is 6x - 6x^2
        Func<float, float> easeDerivative = x => 6f * x * (1 - x);
        return easeDerivative(t);
    }

    public void Toggle()
    {
        Target = (Target) switch { ScaleState.Small => ScaleState.Large, ScaleState.Large => ScaleState.Small };
    }
}
