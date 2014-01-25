using UnityEngine;
using System.Collections;

public class CharSelectGUI : GameGUI
{
    Texture2D mouseIcon;
    Texture2D xboxIcon;
    Texture2D whiteBox;

    /* Settings */
    GUIStyle fontStyle = new GUIStyle();
    float fontHeight;
    GameSettings gameSettings;

    /* Main container */
    float mainContainerLeftMargin;
    float mainContainerTopMargin;
    float mainContainerWidht;
    float mainContainerHeight;

    float mainContrainerWidthBuffer;
    float mainContainerHeightBuffer;

    Rect mainContainerRect;

    /* Player container */
    float playerContainerOffset;
    float playerContainerWidth;
    float playerContainerHeight;
    float playerContainerTopMargin;

    public override void Awake()
    {
        base.Awake();
        gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();
    }

    // Use this for initialization
    void Start()
    {
        mouseIcon = (Texture2D)Resources.Load("mouseIcon");
        xboxIcon = (Texture2D)Resources.Load("xboxIcon");
        whiteBox = (Texture2D)Resources.Load("whiteBox");

        /* Settings */
        fontHeight = fontStyle.CalcHeight(new GUIContent("Random"), 1) + 7;

        /* Main container */
        mainContainerLeftMargin = GUIManager.ResizeGUI(0.1f, GUIManager.DistanceType.Width);
        mainContainerTopMargin = GUIManager.ResizeGUI(0.2f, GUIManager.DistanceType.Height);
        mainContainerWidht = GUIManager.ResizeGUI(0.8f, GUIManager.DistanceType.Width);
        mainContainerHeight = GUIManager.ResizeGUI(0.6f, GUIManager.DistanceType.Height);

        mainContrainerWidthBuffer = GUIManager.ResizeGUI(0.025f, GUIManager.DistanceType.Width);
        mainContainerHeightBuffer = GUIManager.ResizeGUI(0.05f, GUIManager.DistanceType.Height);

        mainContainerRect = new Rect(mainContainerLeftMargin, mainContainerTopMargin, mainContainerWidht, mainContainerHeight);

        /* Player container */
        playerContainerOffset = GUIManager.ResizeGUI(0.2f, GUIManager.DistanceType.Width);
        playerContainerTopMargin = mainContainerTopMargin + mainContainerHeightBuffer;
        playerContainerWidth = GUIManager.ResizeGUI(0.15f, GUIManager.DistanceType.Width);
        playerContainerHeight = GUIManager.ResizeGUI(0.5f, GUIManager.DistanceType.Height);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ADD TO FULL GAME
            if (gameSettings.GetNbPlayerReady() < 1)
            {
                return;
            }

            SwapGUI(GUIManager.GUICommand.Empty);
            Application.LoadLevel("level_01");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // There is already a PC player
            if (gameSettings.GetIsPCPlayerActive() == true)
            {
                return;
            }

            int playerSlot = gameSettings.GetNextAvailableSlot();
            if (playerSlot == -1)
            {
                return;
            }

            PlayerSettings playerSettings = new PlayerSettings();
            playerSettings.SetCharacter(PlayerSettings.Character.Alfredo);
            playerSettings.SetInput(PlayerInput.Controller.PC);
            playerSettings.SetControllerNumber(25);
            playerSettings.SetReady(true);

            gameSettings.ChangePlayerSettings(playerSettings, playerSlot);
        }

        // Controller detection
        for (int i = 1; i <= 11; i++)
        {
            if (Input.GetButtonDown("Controller" + i + "_Detect"))
            {
                // There is already a PC player
                if (gameSettings.GetIsControllerActive(i) == true)
                {
                    return;
                }

                Debug.Log("Controller " + i + " activated");

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

                gameSettings.ChangePlayerSettings(playerSettings, playerSlot);
            }
        }
    }

    void OnGUI()
    {

        /* Temp variable */
        string text = "";

        /* Main container */

        GUI.color = Color.white;
        GUI.DrawTexture(mainContainerRect, whiteBox);

        for (int i = 0; i < 4; i++)
        {
            /* Player container */
            float playerContainerLeftMargin;
            playerContainerLeftMargin = mainContainerLeftMargin + mainContrainerWidthBuffer + (i * playerContainerOffset);

            GUI.color = Color.red;
            Rect playerContainerRect = new Rect(playerContainerLeftMargin, playerContainerTopMargin, playerContainerWidth, playerContainerHeight);
            GUI.DrawTexture(playerContainerRect, whiteBox);

            /* Player number */
            float playerNumberTopMargin;
            playerNumberTopMargin = playerContainerTopMargin + (playerContainerHeight * 0.7f);

            GUI.color = Color.green;
            Rect playerNumberRect = new Rect(playerContainerLeftMargin, playerNumberTopMargin, playerContainerWidth * 0.3f, playerContainerHeight * 0.1f);
            GUI.DrawTexture(playerNumberRect, whiteBox);

            GUI.color = Color.black;
            text = (i+1).ToString();
            GUI.Label(GUIManager.CenteredGUI(playerNumberRect, fontStyle.CalcSize(new GUIContent(text)).x, fontHeight), text);

            /* Player input (PC or Xbox) */
            float playerInputLeftMargin;
            playerInputLeftMargin = playerContainerLeftMargin + (playerContainerWidth * 0.3f);
         
            GUI.color = Color.blue;
            Rect playerInputRect = new Rect(playerInputLeftMargin, playerNumberTopMargin, playerContainerWidth * 0.7f, playerContainerHeight * 0.1f);
            GUI.DrawTexture(playerInputRect, whiteBox);

            GUI.color = Color.white;
            if (gameSettings.GetPlayerSettings(i + 1).GetInput() == PlayerInput.Controller.PC)
            {
                GUI.DrawTexture(GUIManager.CenteredGUI(playerInputRect, 20, 20), mouseIcon);
            }
            else
            {
                GUI.DrawTexture(GUIManager.CenteredGUI(playerInputRect, 20, 20), xboxIcon);
            }
            

            /* Character name */
            float playerNameTopMargin;
            playerNameTopMargin = playerContainerTopMargin + (playerContainerHeight * 0.8f);

            GUI.color = Color.yellow;
            Rect playerNameRect = new Rect(playerContainerLeftMargin, playerNameTopMargin, playerContainerWidth, playerContainerHeight * 0.1f);
            GUI.DrawTexture(playerNameRect, whiteBox);

            GUI.color = Color.black;
            text = gameSettings.GetPlayerSettings(i + 1).GetCharacter().ToString();
            GUI.Label(GUIManager.CenteredGUI(playerNameRect, fontStyle.CalcSize(new GUIContent(text)).x, fontHeight + 5), text);

            /* Ready */
            float playerReadyTopMargin;
            playerReadyTopMargin = playerContainerTopMargin + (playerContainerHeight * 0.9f);

            GUI.color = Color.magenta;
            Rect playerReadyRect = new Rect(playerContainerLeftMargin, playerReadyTopMargin, playerContainerWidth, playerContainerHeight * 0.1f);
            GUI.DrawTexture(playerReadyRect, whiteBox);

            GUI.color = Color.black;

            if (gameSettings.GetPlayerSettings(i + 1).GetReady())
            {
                text = "Ready!";
            }
            else
            {
                text = "";
            }

            GUI.Label(GUIManager.CenteredGUI(playerReadyRect, fontStyle.CalcSize(new GUIContent(text)).x, fontHeight + 5), text);       
        }
    }
}
