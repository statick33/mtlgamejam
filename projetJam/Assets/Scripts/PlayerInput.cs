using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour 
{
    public enum Inputs
    {
        GrabRelease, GrabRelease2, Throw, Throw2, Up, Down, Left, Right, UpDown, LeftRight, LookAtX, LookAtY
    };

    public enum Controller
    {
        PC,Xbox,None
    };


    // Remove public when finished
    // Player's number (1,2,3,4)
    public int player;
    public Controller controller;
    
    //Input
    float lastGrabAxisValue = 0;
    float lastThrowAxisValue = 0;

    public Pawn pawn;

	// Use this for initialization
	void Start () 
    {

	}

    void Awake()
    {
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        float grabAxisValue = 0;
        float throwAxisValue = 0;

        // Left and Right bumper
        if (controller == PlayerInput.Controller.Xbox)
        {
            float axisValue = Input.GetAxis(GetInput(PlayerInput.Inputs.GrabRelease2));

            if (axisValue > 0)
            {
                grabAxisValue = axisValue;
                throwAxisValue = 0;
            }
            else
            {
                grabAxisValue = 0;
                throwAxisValue = Mathf.Abs(axisValue);
            }
        }

        if (Input.GetButtonDown(GetInput(PlayerInput.Inputs.GrabRelease)) || ((controller == PlayerInput.Controller.Xbox) && grabAxisValue >= 0.1 && lastGrabAxisValue < 0.1))
        {
            //pawn.Action(Pawn.PlayerAction.GrabDrop);
        }
        else if (Input.GetButtonDown(GetInput(PlayerInput.Inputs.Throw)) || ((controller == PlayerInput.Controller.Xbox) && throwAxisValue >= 0.1 && lastThrowAxisValue < 0.1))
        {
            //pawn.Action(Pawn.PlayerAction.Throw);
        }

        lastGrabAxisValue = grabAxisValue;
        lastThrowAxisValue = throwAxisValue;
	}

    public void SetLockInput(bool pLock)
    {
        pawn.SetLockAction(pLock,false);
    }

    public void SetPlayerNumber(int pPlayer)
    {
        player = pPlayer;
    }

    public int GetPlayerNumber()
    {
        return player;
    }

    public void SetPlayerController(Controller pController)
    {
        controller = pController;
    }

    public Controller GetPlayerController()
    {
        return controller;
    }

    public string GetInput(Inputs pRequestedInput)
    {
        if (controller == Controller.PC)
        {
            return "PC_" + pRequestedInput;
        }
        else if (controller == Controller.Xbox)
        {
            // Ex : Controller2_Throw --> Controller player2 throw
            return "Controller" + player + "_" + pRequestedInput;
        }
        return "";
    }
}
