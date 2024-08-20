using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGunRay : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer lineRenderer;
    public int startingCasts;
    int casts;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        casts = startingCasts;
    }

    public bool CanShootScalable(Scalable scalable) => casts > 0 || scalable.stateOnStart == scalable.Target;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.TransformDirection(Vector3.up);
        RaycastHit hit;
        LayerMask mask = ~LayerMask.GetMask("Ignore Raycast");
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, mask))
        {
            // TODO: Create a LineRenderer
            lineRenderer.SetPositions(new Vector3[2] { transform.position, hit.point });
            lineRenderer.startColor = lineRenderer.endColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);
            hit.collider.gameObject.TryGetComponent(out Scalable scalable);
            if (scalable != null)
            {
                bool shootingAnotherCast = scalable.stateOnStart == scalable.Target;
                bool mouseDown = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q);
                if (shootingAnotherCast)
                {
                    if (casts > 0)
                    {
                        if (mouseDown)
                        {
                            scalable.Toggle();
                            casts--;
                        }
                        lineRenderer.startColor = lineRenderer.endColor = new Color(1, 1, 1, 1);
                    }
                    else lineRenderer.startColor = lineRenderer.endColor = new Color(1, 0, 0, 1);
                }
                else
                {
                    if (mouseDown)
                    {
                        casts++;
                        scalable.Toggle();
                    }
                    lineRenderer.startColor = lineRenderer.endColor = new Color(0, 1, 0, 1);
                }
            }
        } else
        {
            lineRenderer.SetPositions(new Vector3[2] { transform.position, transform.position + direction * 500f });
            lineRenderer.startColor = lineRenderer.endColor = new Color(1, 1, 1, 0);
        }
    }
}
