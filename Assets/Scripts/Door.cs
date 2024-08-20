using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public StateChanger<bool> toggle;
    public bool activeAtThisState;
    bool CurrentlyActive { get => toggle.State == activeAtThisState; }
    BoxCollider collider;
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        renderer = GetComponent<Renderer>();
        toggle.OnStateSwitch += (object sender, bool currentState) =>
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

    private void Update()
    {
        // FIXME: removed a line
        // if (collider.enabled && Physics.OverlapBox(transform.position, transform.lossyScale, transform.rotation, 4).Any(x => x.CompareTag("Respawn")))
        if (false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
