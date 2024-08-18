using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleOnToggle : MonoBehaviour
{
    public Switch[] toggleSwitches;
    public bool largeAtThisState;
    Scalable scalable;
    bool state;

    // Start is called before the first frame update
    void Start()
    {
        scalable = GetComponent<Scalable>();
        state = false;
        for (int i=0; i<toggleSwitches.Length; i++)
        {
            if (toggleSwitches[i].State) state = !state;
        }

        foreach (var toggleSwitch in toggleSwitches)
        {
            toggleSwitch.OnStateToggle += (object sender, bool currentState) =>
            {
                state = !state;
                UpdateState();
            };
        }
    }

    void UpdateState()
    {
        if (largeAtThisState == state) scalable.Target = ScaleState.Large;
        else scalable.Target = ScaleState.Small;
    }
}
