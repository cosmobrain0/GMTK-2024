using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGunRay : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.TransformDirection(-Vector3.right);
        RaycastHit hit;
        LayerMask mask = ~LayerMask.GetMask("Ignore Raycast");
        Debug.Log(~mask);
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, mask))
        {
            // TODO: Create a LineRenderer
            lineRenderer.SetPositions(new Vector3[2] { transform.position, hit.point });
            lineRenderer.startColor = lineRenderer.endColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);
            hit.collider.gameObject.TryGetComponent(out Scalable scalable);
            if (scalable != null)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q)) scalable.Toggle();
                lineRenderer.startColor = lineRenderer.endColor = new Color(1, 1, 1, 1);
            }
        } else
        {
            lineRenderer.SetPositions(new Vector3[2] { transform.position, transform.position + direction * 500f });
            lineRenderer.startColor = lineRenderer.endColor = new Color(1, 1, 1, 0);
        }
    }
}
