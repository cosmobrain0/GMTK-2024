using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float horizontalSpeed;
    public float gravity;
    public float jumpSpeed;
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.zero;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 accelaration = new Vector2(
            0,
            gravity
        );
        if (characterController.isGrounded)
        {
            velocity.x = Input.GetAxis("Horizontal") * horizontalSpeed;
            velocity.y = Mathf.Max(velocity.y, 0);
            if (Input.GetAxis("Jump") != 0)
                velocity.y += jumpSpeed;
        }
        velocity += accelaration * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime); 
    }
}
