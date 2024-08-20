using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public enum CombineMethod
{
    XOR,
    OR,
    AND
}

public class CombineSwitches : StateChanger<bool>
{
    public override event EventHandler<bool> OnStateSwitch;
    public StateChanger<bool>[] toggles;
    public CombineMethod combineMethod;
    bool _state;
    public override bool State { get => _state; }
    // Start is called before the first frame update
    void Start()
    {
        UpdateState();

        foreach (var toggleSwitch in toggles)
        {
            toggleSwitch.OnStateSwitch += (object sender, bool currentState) =>
            {
                UpdateState();
            };
        }
    }

    void UpdateState()
    {
        IEnumerable<bool> values = toggles.Select(x => x.State);
        bool previousState = _state;
        switch (combineMethod)
        {
            case CombineMethod.AND:
                UpdateStateAnd(values);
                break;
            case CombineMethod.OR:
                UpdateStateOr(values);
                break;
            case CombineMethod.XOR:
                UpdateStateXor(values);
                break;
        }
        if (previousState != _state)
        {
            OnStateSwitch?.Invoke(this, _state);
        }
    }

    private void UpdateStateXor(IEnumerable<bool> values)
    {
        _state = values.Where(x => x).Count() % 2 == 1;
    }

    private void UpdateStateOr(IEnumerable<bool> values)
    {
        _state = values.Any(x => x);
    }

    private void UpdateStateAnd(IEnumerable<bool> values)
    {
        _state = values.All(x => x);
    }
}
