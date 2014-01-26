using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharSelectGUI : GameGUI
{
    Texture2D mouseIcon;
    Texture2D xboxIcon;
    Texture2D whiteBox;

    Texture2D pressStart;

    Texture2D pressLT;

    Texture2D cameleon1_pressRT;
    Texture2D cameleon1_ready;

    Texture2D cameleon2_pressRT;
    Texture2D cameleon2_ready;

    Texture2D cameleon3_pressRT;
    Texture2D cameleon3_ready;

    Texture2D cameleon4_pressRT;
    Texture2D cameleon4_ready;

    Texture2D background;


    /* Settings */
    GUIStyle fontStyle = new GUIStyle();
    float fontHeight;
    GameSettings gameSettings;

    /* Main container */
    float mainContainerLeftMargin;
    float mainContainerTopMargin;
    float mainContainerWidht;
    float mainContainerHeight;

    float mainContrainerHeightBuffer;
    float mainContainerHeightBuffer;

    Rect mainContainerRect;

    /* Player container */
    float playerContainerOffset;
    float playerContainerWidth;
    float playerContainerHeight;
    float playerContainerTopMargin;

    List<Texture2D> listCamelionPressRt = new List<Texture2D>();
    List<Texture2D> listCamelionReady = new List<Texture2D>();

    List<float> listLastThrowAxisValue = new List<float>();
    List<float> listLastGrabAxisValue = new List<float>();


    int pcPlayerSlot;

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

        pressStart = (Texture2D)Resources.Load("press_start");
        pressLT = (Texture2D)Resources.Load("press_LT");

        
        listCamelionPressRt.Add((Texture2D)Resources.Load("cameleon1_pressRT"));
        listCamelionPressRt.Add((Texture2D)Resources.Load("cameleon2_pressRT"));
        listCamelionPressRt.Add((Texture2D)Resources.Load("cameleon3_pressRT"));
        listCamelionPressRt.Add((Texture2D)Resources.Load("cameleon4_pressRT"));

        
        listCamelionReady.Add((Texture2D)Resources.Load("cameleon1_ready"));
        listCamelionReady.Add((Texture2D)Resources.Load("cameleon2_ready"));
        listCamelionReady.Add((Texture2D)Resources.Load("cameleon3_ready"));
        listCamelionReady.Add((Texture2D)Resources.Load("cameleon4_ready"));

        background = (Texture2D)Resources.Load("bg_startScreen");

        /* Settings */
        fontHeight = fontStyle.CalcHeight(new GUIContent("Random"), 1) + 7;

        /* Main container */
        mainContainerLeftMargin = 0;
        mainContainerTopMargin = 0;
        mainContainerWidht = GUIManager.ResizeGUI(1.0f, GUIManager.DistanceType.Width) +1;
        mainContainerHeight = GUIManager.ResizeGUI(1.0f, GUIManager.DistanceType.Height) +1;

        mainContrainerHeightBuffer = 0.0f;
        mainContainerHeightBuffer = GUIManager.ResizeGUI(0.01f, GUIManager.DistanceType.Height);

        mainContainerRect = new Rect(mainContainerLeftMargin, mainContainerTopMargin, mainContainerWidht, mainContainerHeight);

        /* Player container */
        playerContainerOffset = 128.0f;
        playerContainerTopMargin = mainContainerTopMargin + mainContainerHeightBuffer;
        playerContainerWidth = GUIManager.ResizeGUI(0.45f, GUIManager.DistanceType.Width);
        playerContainerHeight = GUIManager.ResizeGUI(0.20f, GUIManager.DistanceType.Height);

        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);
        listLastThrowAxisValue.Add(0);

        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
        listLastGrabAxisValue.Add(0);
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

            SwapGUI(GUIManager.GUICommand.InGame);
            Application.LoadLevel("level_01");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // There is already a PC player
            if (gameSettings.GetIsPCPlayerActive() == true)
            {
                PlayerSettings playerSet = gameSettings.GetPlayerSettings(pcPlayerSlot);
                playerSet.SetReady(true);
                gameSettings.ChangePlayerSettings(playerSet, pcPlayerSlot);

                return;
            }

            int playerSlot = gameSettings.GetNextAvailableSlot();
            if (playerSlot == -1)
            {
                return;
            }

            if (gameSettings.GetPlayerSettings(playerSlot).GetControllerNumber() == 0)
            {
                PlayerSettings playerSettings = new PlayerSettings();
                playerSettings.SetCharacter(PlayerSettings.Character.Alfredo);
                playerSettings.SetInput(PlayerInput.Controller.PC);
                playerSettings.SetControllerNumber(25);

                pcPlayerSlot = playerSlot;

                gameSettings.ChangePlayerSettings(playerSettings, playerSlot);
            }    
       }

        // Controller detection
        for (int i = 1; i <= 11; i++)
        {
            float grabAxisValue = 0;
            float throwAxisValue = 0;

            if (Input.GetButtonDown("Controller" + i + "_Detect"))
            {
                Debug.Log("Controller" + i + "_Detect");

                if (gameSettings.GetIsControllerActive(i) == false)
                {
                    int playerSlot = gameSettings.GetNextAvailableSlot();

                    if (playerSlot == -1)
                    {
                        return;
                    }


                    PlayerSettings playerSettings = new PlayerSettings();
                    playerSettings.SetCharacter(PlayerSettings.Character.Louis);
                    playerSettings.SetInput(PlayerInput.Controller.Xbox);
                    playerSettings.SetControllerNumber(i);

                    gameSettings.ChangePlayerSettings(playerSettings, playerSlot);
                    
                }
                
            }
            else 
            {
                float axisValue = Input.GetAxis("Controller" + i + "_GrabRelease2");

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

                if (grabAxisValue >= 0.1 && listLastGrabAxisValue[i - 1] < 0.1)
                {
                    // ADD TO FULL GAME
                    if (gameSettings.GetNbPlayerReady() < 1)
                    {
                        return;
                    }

                    SwapGUI(GUIManager.GUICommand.InGame);
                    Application.LoadLevel("level_01");
                    return;
                }

                if (throwAxisValue >= 0.1 && listLastThrowAxisValue[i - 1] < 0.1)
                {
                    // There is already a player
                    if (gameSettings.GetIsControllerActive(i) == true)
                    {
                        PlayerSettings playerSet = gameSettings.GetPlayerSettings(gameSettings.ControllerNumberToPlayerNumber(i));
                        playerSet.SetReady(true);
                        gameSettings.ChangePlayerSettingsByControllerNumber(playerSet, i);
                        return;
                    }

                    Debug.Log("Controller " + i + " activated");
                }
            }

            listLastThrowAxisValue[i - 1] = throwAxisValue;
            listLastGrabAxisValue[i - 1] = grabAxisValue;

            /*
            if (Input.GetButtonDown("Controller" + i + "_Detect"))
            {
                
            }*/
        }
    }

    void OnGUI()
    {
        /* Main container */

        GUI.DrawTexture(mainContainerRect, background);

        for (int i = 0; i < 4; i++)
        {
            /* Player container */
            float playerContainerTopMargin;
            playerContainerTopMargin = (i * playerContainerOffset);

            Texture2D readyTextutre;
            if (gameSettings.GetPlayerSettings(i + 1).GetControllerNumber() != 0)
            {
                if (gameSettings.GetPlayerSettings(i + 1).GetReady())
                {
                    readyTextutre = listCamelionReady[i];
                }
                else
                {
                    readyTextutre = listCamelionPressRt[i];
                }
            }
            else
            {
                readyTextutre = pressStart;
            }  

            //GUI.color = Color.red;
            Rect playerContainerRect = new Rect(GUIManager.ResizeGUI(0.55f, GUIManager.DistanceType.Width),80 + (GUIManager.ResizeGUI(0.21f, GUIManager.DistanceType.Height)) * i, (GUIManager.ResizeGUI(0.33f, GUIManager.DistanceType.Width)), (GUIManager.ResizeGUI(0.20f, GUIManager.DistanceType.Height)));
            GUI.DrawTexture(playerContainerRect, readyTextutre);

            /* Player number */
            float playerNumberTopMargin;
            playerNumberTopMargin = playerContainerTopMargin + (playerContainerHeight * 0.7f);

            /* Player input (PC or Xbox) */
            float playerInputLeftMargin;
            playerInputLeftMargin = playerContainerTopMargin + (playerContainerWidth * 0.3f);
         
            /* Character name */
            float playerNameTopMargin;
            playerNameTopMargin = playerContainerTopMargin + (playerContainerHeight * 0.8f);
 
            /* Ready */
            float playerReadyTopMargin;
            playerReadyTopMargin = playerContainerTopMargin + (playerContainerHeight * 0.9f);

            if (i == 3 && gameSettings.GetNbPlayerReady() > 1)
            {
                Rect startGameRect = new Rect(GUIManager.ResizeGUI(0.15f, GUIManager.DistanceType.Width), (GUIManager.ResizeGUI(0.80f, GUIManager.DistanceType.Height)), (GUIManager.ResizeGUI(0.20f, GUIManager.DistanceType.Width)), (GUIManager.ResizeGUI(0.15f, GUIManager.DistanceType.Height)));
                GUI.DrawTexture(startGameRect, pressLT);
            }
                
        }
    }
}
