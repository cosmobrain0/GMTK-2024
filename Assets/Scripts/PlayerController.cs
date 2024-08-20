#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float horizontalSpeed;
    public float horizontalAirAccelaration;
    public float maxHorizontalSpeed;
    public float gravity;
    public float jumpSpeed;
    public float bottomY;
    public float coyoteTime;
    public float crushDetectionDistance;
    public float teleportDistance;
    Vector3 previousPosition;
    DateTime? lastTimeOnGround;
    bool jumpedSinceLastTimeOnGround;
    Vector2 velocity;
    Goal goal;

    Vector2 pushVelocity;
    public float pushDragCoefficient;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
        lastTimeOnGround = null;
        jumpedSinceLastTimeOnGround = false;
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
        if ((transform.position-previousPosition).magnitude >= teleportDistance)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        previousPosition = transform.position;

        Vector2 accelaration = new Vector2(
            0,
            gravity
        );
        if (characterController.velocity.x == 0f)
        {
            velocity.x = 0f;
            pushVelocity.x = 0f;
        }
        velocity.x = Input.GetAxis("Horizontal") * horizontalSpeed;
        if (characterController.isGrounded)
        {
            jumpedSinceLastTimeOnGround = false;
            lastTimeOnGround = DateTime.Now;
            velocity.y = Mathf.Max(velocity.y, 0);
            pushVelocity.y = Mathf.Max(pushVelocity.y, 0);
        }
        velocity += accelaration * Time.deltaTime;

        if (CanJump())
        {
            if (Input.GetAxis("Jump") != 0 || Input.GetAxis("Vertical") > 0)
            {
                jumpedSinceLastTimeOnGround = true;
                velocity.y = jumpSpeed;
                pushVelocity.y = 0;
            }
        }

        pushVelocity *= Mathf.Exp(-pushDragCoefficient * Time.deltaTime);

        characterController.Move((velocity+pushVelocity) * Time.deltaTime);

        if (transform.position.y < bottomY) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
        {
            Vector3[] directions = { Vector3.right, Vector3.up, Vector3.left, Vector3.down };

            for (int i=0; i<directions.Length; i++)
            {
                if (Physics.Raycast(transform.position, directions[i], crushDetectionDistance, ~12))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

        }
    }

    private bool CanJump()
    {
        return !jumpedSinceLastTimeOnGround && (
                characterController.isGrounded ||
                (lastTimeOnGround != null && (DateTime.Now - lastTimeOnGround)?.TotalMilliseconds < coyoteTime)
            );
    }
}
