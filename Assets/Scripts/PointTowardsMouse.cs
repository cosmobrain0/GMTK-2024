using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowardsMouse : MonoBehaviour
{
    public Vector3 forward;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 position3D = camera.WorldToScreenPoint(transform.position);
        Vector2 position = new Vector2(position3D.x, position3D.y);
        Vector2 offset = position - mouse;
        float angle = Mathf.Atan2(offset.y, offset.x);
        transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg) * Quaternion.LookRotation(forward);
    }
}
