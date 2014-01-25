using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartGameGUI : GameGUI
{
    Texture2D selector;

    Vector2 menuLocation;
    List<GUIButton> listButton = new List<GUIButton>();
    int selectedButtonIndex;

    LeftAnalogInterface analog;

	// Use this for initialization
	void Start () 
    {

        /* Controller input */

        analog = gameObject.AddComponent<LeftAnalogInterface>();
        analog.Init("ControllersUpDown", "ControllersLeftRight");
        analog.OnDirectionChange += new LeftAnalogInterface.LeftAnalogHandler(ChangeGUIFocus);

        /* Selector texture */

	    selector = (Texture2D)Resources.Load("whiteBox");

        /* DONT forget to change the first param depending on how many GUI element you have in this menu*/
        menuLocation = menuLocation = GUIManager.MenuLocationGUI(3, GUISettings.buttonWidth, GUISettings.buttonHeight, GUISettings.buttonOffset);


        /* Buttons */
        Rect buttonLocation = new Rect(menuLocation.x, menuLocation.y, GUISettings.buttonWidth, GUISettings.buttonHeight);

        // Play
        GUIButton playButton = new GUIButton();

        playButton.controlName = GUIManager.GUICommand.Play.ToString();
        playButton.displayName = "Play";
        playButton.position = buttonLocation;
        playButton.style = GUIManager.GetGUIStyle();

        listButton.Add(playButton);

        // Options

        buttonLocation = GUIManager.OffsetGUI(buttonLocation, GUISettings.buttonOffset);

        GUIButton optionsButton = new GUIButton();

        optionsButton.controlName = GUIManager.GUICommand.Options.ToString();
        optionsButton.displayName = "Options";
        optionsButton.position = buttonLocation;
        optionsButton.style = GUIManager.GetGUIStyle();

        listButton.Add(optionsButton);

        // Exit
        buttonLocation = GUIManager.OffsetGUI(buttonLocation, GUISettings.buttonOffset);

        GUIButton exitButton = new GUIButton();

        exitButton.controlName = GUIManager.GUICommand.Quit.ToString();
        exitButton.displayName = "Quit";
        exitButton.position = buttonLocation;
        exitButton.style = GUIManager.GetGUIStyle();

        listButton.Add(exitButton);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            listButton[selectedButtonIndex].pressed = true;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedButtonIndex > 0)
            {
                selectedButtonIndex -= 1;
            }
            else
            {
                selectedButtonIndex = listButton.Count - 1;
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedButtonIndex < listButton.Count - 1)
            {
                selectedButtonIndex = selectedButtonIndex + 1;
            }
            else
            {
                selectedButtonIndex = 0;
            }
        }
    }
	

    void OnGUI()
    {
        GUI.SetNextControlName(listButton[0].controlName);
        if (GUI.Button(listButton[0].position, listButton[0].displayName,listButton[0].style) || listButton[0].pressed)
        {
            listButton[0].pressed = false;
            SwapGUI(GUIManager.GUICommand.Play);
            return;
        }

        GUI.SetNextControlName(listButton[1].controlName);
        if (GUI.Button(listButton[1].position, listButton[1].displayName, listButton[1].style) || listButton[1].pressed)
        {
            listButton[1].pressed = false;
            //SwapGUI(GUIManager.GUICommand.Play);
            return;
        }

        GUI.SetNextControlName(listButton[2].controlName);
        if (GUI.Button(listButton[2].position, listButton[2].displayName, listButton[2].style) || listButton[2].pressed)
        {
            listButton[2].pressed = false;
            SwapGUI(GUIManager.GUICommand.Quit);
            return;
        }

        /* Buttons hover */
        for (int i = 0; i < listButton.Count; i++)
        {
            if (listButton[i].position.Contains(MousePosition2D()))
            {
                selectedButtonIndex = i;
                break;
            }
        }

        /* Draw selector */
        GUI.FocusControl(listButton[selectedButtonIndex].controlName);

        Rect selectorLocation;
        selectorLocation = listButton[selectedButtonIndex].position;

        selectorLocation.x += listButton[selectedButtonIndex].position.width + 10;

        selectorLocation.width = 20;
        selectorLocation.height = 20;

        GUI.DrawTexture(selectorLocation, selector);        
    }

    public void ChangeGUIFocus(LeftAnalogInterface.LeftAnalogDirection pDirection)
    {
        switch (pDirection)
        {
            case LeftAnalogInterface.LeftAnalogDirection.Up:
                if (selectedButtonIndex > 0)
                {
                    selectedButtonIndex -= 1;
                }
                else
                {
                    selectedButtonIndex = listButton.Count - 1;
                }
                break;
            case LeftAnalogInterface.LeftAnalogDirection.Down:
                if (selectedButtonIndex < listButton.Count - 1)
                {
                    selectedButtonIndex = selectedButtonIndex + 1;
                }
                else
                {
                    selectedButtonIndex = 0;
                }
                break;
            default:
                break;
        }
    }
}
