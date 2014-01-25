using UnityEngine;
using System.Collections;

public class TopController : MonoBehaviour 
{

    public float rotationSpeed = 7;

    PlayerInput playerInput;

    // Previous direction player was facing
    float lastLookAtX, lastLookAtY = 1;

    // PC mouse position or controller joystick direction
    Vector3 lookAtPosition = Vector3.zero;

    Pawn pawn;

	// Use this for initialization
	void Start () 
    {
        playerInput = transform.parent.GetComponent<PlayerInput>();

        //Debug.Log(" 0 :"+Input.GetJoystickNames()[0] + " 1 :"+ Input.GetJoystickNames()[1]);

        pawn = transform.parent.GetComponent<Pawn>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        /* Rotate the top half body */

        if (pawn.GetLockAction())
        {
            return;
        }

        // PC
        if (playerInput.controller == PlayerInput.Controller.PC)
        {
           // Mouse location


            lookAtPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y - transform.position.y));
        }

        // Controller
        else
        {
            // Used to be var

            float lookX = Input.GetAxis(playerInput.GetInput(PlayerInput.Inputs.LookAtX));
            float lookY = -Input.GetAxis(playerInput.GetInput(PlayerInput.Inputs.LookAtY));

 
            // Skip value from inner circle (X)
            if (Mathf.Abs(lookX) <= 0.95 && (Mathf.Abs(lookX) + Mathf.Abs(lookY)) < 0.95f)
            {
                lookX = 0;
            }

            // Skip value from inner circle (Y)
            if (Mathf.Abs(lookY) <= 0.95 && (Mathf.Abs(lookX) + Mathf.Abs(lookY)) < 0.95f)
            {
                lookY = 0;
            }

            // If the right analog is released, look at the last known direction
            if (lookX == 0 && lookY == 0)
            {
                lookX = lastLookAtX;
                lookY = lastLookAtY;
            }

            //Debug.Log("       X :" + lookX + "        Y: " + lookY + "\nOLD-X :" + lastLookAtX + " OLD-Y: " + lastLookAtY);

            lookAtPosition = transform.position + new Vector3(lookX, 0, lookY) * 100;


            lastLookAtX = lookX;
            lastLookAtY = lookY;
        }
        
        // Rotation calculation

        Quaternion rotation = Quaternion.LookRotation(lookAtPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

	}
}
