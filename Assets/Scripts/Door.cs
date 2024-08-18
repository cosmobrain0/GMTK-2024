using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Switch toggleSwitch;
    public bool activeAtThisState;
    bool CurrentlyActive { get => toggleSwitch.State == activeAtThisState; }
    BoxCollider collider;
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        renderer = GetComponent<Renderer>();
        toggleSwitch.OnStateSwitch += (object sender, bool currentState) =>
        {
            if (activeAtThisState == currentState)
            {
                collider.enabled = true;
                renderer.material.color = new Color(1, 1, 1, 1);
            }
            else
            {
                collider.enabled = false;
                renderer.material.color = new Color(1, 1, 1, 0.4f);
            }
        };
    }
}
