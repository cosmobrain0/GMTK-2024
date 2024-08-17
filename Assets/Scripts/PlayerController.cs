using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float horizontalSpeed;
    public float horizontalAirAccelaration;
    public float maxHorizontalSpeed;
    public float gravity;
    public float jumpSpeed;
    Vector2 velocity;
    Goal goal;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.zero;
        characterController = GetComponent<CharacterController>();
        goal = FindAnyObjectByType<Goal>();
        goal.OnLevelComplete += (object sender, EventArgs e) =>
        {
            Debug.Log("Yaaaaay!");
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (goal.LevelCompleted) return;

        Vector2 accelaration = new Vector2(
            0,
            gravity
        );
        if (characterController.velocity.x == 0f) velocity.x = 0f;
        if (characterController.isGrounded)
        {
            velocity.x = Input.GetAxis("Horizontal") * horizontalSpeed;
            velocity.y = Mathf.Max(velocity.y, 0);
            if (Input.GetAxis("Jump") != 0)
                velocity.y += jumpSpeed;
        } else
        {
            accelaration.x += Input.GetAxis("Horizontal") * horizontalAirAccelaration;
            velocity.x = Mathf.Clamp(velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        }
        velocity += accelaration * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime); 
    }
}
