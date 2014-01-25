using UnityEngine;
using System.Collections;

public class FullBodyController : MonoBehaviour 
{
    public float movementSpeed = 0.4f;
    public CharacterController controller;

    public PlayerInput playerInput;

    // Lock Y position
    float originalY;

    public Pawn pawn;

    public Vector3 direction;
    private float directionBuffer = 10.0f;

	// Use this for initialization
	void Start () 
    {
        originalY = transform.position.y;      
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (pawn.GetLockAction())
        {
            return;
        }

        // PC
        if (playerInput.controller == PlayerInput.Controller.PC)
        {
            // Up
            if (Input.GetButton(playerInput.GetInput(PlayerInput.Inputs.Up)))
            {
                direction = new Vector3(0, 0, 1 * directionBuffer);
                controller.Move(new Vector3(0, 0, movementSpeed));
            }

            // Down
            if (Input.GetButton(playerInput.GetInput(PlayerInput.Inputs.Down)))
            {
                direction = new Vector3(0, 0, -1 * directionBuffer);
                controller.Move(new Vector3(0, 0, -movementSpeed));
            }

            // Left
            if (Input.GetButton(playerInput.GetInput(PlayerInput.Inputs.Left)))
            {
                direction = new Vector3(-1 * directionBuffer, 0, 0);
                controller.Move(new Vector3(-movementSpeed, 0, 0));
            }

            // Right
            if (Input.GetButton(playerInput.GetInput(PlayerInput.Inputs.Right)))
            {
                direction = new Vector3(1 * directionBuffer, 0, 0);
                controller.Move(new Vector3(movementSpeed, 0, 0));
            }
        }

        // Controller
        else
        {
            float upDown;
            upDown = Input.GetAxis(playerInput.GetInput(PlayerInput.Inputs.UpDown));


            if (Mathf.Abs(upDown) < 0.2)
            {
                upDown = 0;
            }

            if (upDown > 0)
            {
                direction = new Vector3(0, 0, -1 * directionBuffer);
            }
            else
            {
                direction = new Vector3(0, 0, 1 * directionBuffer);
            }

            controller.Move(new Vector3(0, 0, upDown * -movementSpeed));


            float leftRight;
            leftRight = Input.GetAxis(playerInput.GetInput(PlayerInput.Inputs.LeftRight));

            if (Mathf.Abs(leftRight) < 0.2)
            {
                leftRight = 0;
            }

            if (leftRight > 0)
            {
                direction = new Vector3(1 * directionBuffer, 0, 0);
            }
            else
            {
                direction = new Vector3(-1 * directionBuffer, 0, 0);
            }

            controller.Move(new Vector3(leftRight * movementSpeed, 0, 0));

        }

        // Lock Y position
        if (transform.position.y != originalY)
        {
            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        }
	}
}
