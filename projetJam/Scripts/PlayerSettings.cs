using UnityEngine;
using System.Collections;

public class PlayerSettings
{

    public enum Character
    {
        None,
        Juan,
        Louis,
        Talisa,
        Alfredo 
    };

    private Character selectedChar = Character.None;
    private PlayerInput.Controller selectedInput = PlayerInput.Controller.None;
    private int controllerNumber = 0;
    private bool ready = false;

    public void SetCharacter(Character pCharacter)
    {
        selectedChar = pCharacter;
    }

    public Character GetCharacter()
    {
        return selectedChar;
    }

    public void SetInput(PlayerInput.Controller pInput)
    {
        selectedInput = pInput;
    }

    public PlayerInput.Controller GetInput()
    {
        return selectedInput;
    }

    public void SetControllerNumber(int pNumber)
    {
        controllerNumber = pNumber;
    }

    public int GetControllerNumber()
    {
        return controllerNumber;
    }

    public void SetReady(bool pReady)
    {
        ready = pReady;
    }

    public bool GetReady()
    {
        return ready;
    }
}
