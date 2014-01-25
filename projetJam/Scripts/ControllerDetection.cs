using UnityEngine;
using System.Collections;

public class ControllerDetection : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Controller detection
        for (int i = 1; i <= 11; i++)
        {
            if (Input.GetButtonDown("Controller" + i + "_Detect"))
            {
                /*
                // There is already a PC player
                if (gameSettings.GetIsControllerActive(i) == true)
                {
                    return;
                }*/

                Debug.Log("Controller " + i + " activated");
                /*
                int playerSlot = gameSettings.GetNextAvailableSlot();


                if (playerSlot == -1)
                {
                    return;
                }

                PlayerSettings playerSettings = new PlayerSettings();
                playerSettings.SetCharacter(PlayerSettings.Character.Louis);
                playerSettings.SetInput(PlayerInput.Controller.Xbox);
                playerSettings.SetControllerNumber(i);
                playerSettings.SetReady(true);

                gameSettings.ChangePlayerSettings(playerSettings, playerSlot);*/
            }
        }
	}
}
