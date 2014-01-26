using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GameGUIStyle))]
public class GUIManager : MonoBehaviour 
{
    public static GUIManager instance;

    public static GUIStyle style;
    public enum GUICommand
    {
        StartGame, Play, Options, Quit, Ready, Empty, InGame, Tally
    }
    public enum DistanceType 
    {
        Width,Height
    }
    Dictionary<GUICommand, GameGUI> dictGUI = new Dictionary<GUICommand, GameGUI>();
    GUICommand currentGUI = GUICommand.StartGame;

    public Vector2 mousePosition2D;


    bool isDrawing = true;

    List<PlayerInput> listPlayerInput = new List<PlayerInput>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }

        GameGUIStyle obj = gameObject.GetComponent<GameGUIStyle>();

        if (obj != null)
        {
            style = obj.style;
        }
        else
        {
            style = new GUIStyle();
        }
    }

	// Use this for initialization
	void Start () 
    {
        /* GOOD */

        dictGUI.Add(GUICommand.StartGame, new StartGameGUI());
        dictGUI.Add(GUICommand.Play, new CharSelectGUI());
        dictGUI.Add(GUICommand.Ready, new LevelSelectGUI());
        dictGUI.Add(GUICommand.Empty, new EmptyGUI());
        dictGUI.Add(GUICommand.InGame, new InGameGUI());
        dictGUI.Add(GUICommand.Tally, new TallyGUI());

        if (Application.loadedLevelName == "StartGame")
        {
            SwapGUI(GUICommand.StartGame);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Mouse position on GUI
        mousePosition2D = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

        //Debug.Log("MY ID : " +GetInstanceID().ToString());

        // Start game
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isDrawing = false;
            SetPlayersInputLock(false);
        }

        if (isDrawing == false)
        {
            return;
        }
	}

    void OnGUI()
    {

    }

    /* GUI */

    public void SwapGUI(GUICommand pCommand)
    {
        // Exit game
        if (pCommand == GUICommand.Quit)
        {
            Application.Quit();
            return;
        }

        // Remove the currently displayed GUI (remove from component)
        GameGUI exitGUI;
        bool foundGUI = dictGUI.TryGetValue(currentGUI, out exitGUI);

        if (foundGUI)
        {
            Destroy(GetComponent(exitGUI.GetType()));
        }

        // Add the asked GUI to the component and draw it
        GameGUI drawGUI;

        foundGUI = dictGUI.TryGetValue(pCommand, out drawGUI);

        if (foundGUI)
        {
            currentGUI = pCommand;
            gameObject.AddComponent(drawGUI.GetType());
        }
    }

    /// <summary>
    /// Resize GUI with the current resolution.
    /// </summary>
    /// <param name="pDistance">Distance in % of the screen resolution (Ex : 10% = 0.1f)</param>
    /// <param name="pType">Type of scaling</param>
    /// <returns>Scaled distance based on the screen resolution</returns>
    public static float ResizeGUI(float pDistance, DistanceType pType)
    {
        float distance;

        if (pType == DistanceType.Width)
        {
            distance = pDistance * Screen.width;
        }
        else
        {
            distance = pDistance * Screen.height;
        }

        return distance;
    }

    /// <summary>
    /// Offset the GUI from the location given. The scale of the rectangle isn't changed.
    /// </summary>
    /// <param name="pCurrentLocation">The location to apply the offset</param>
    /// <param name="pHeightOffset">The height of the offset in pixel</param>
    /// <returns>New location of the GUI with the same scale</returns>
    public static Rect OffsetGUI(Rect pCurrentLocation, float pHeightOffset)
    {
        float height = pCurrentLocation.height;
        float top = pCurrentLocation.yMin;

        float newHeightLocation = top + height + pHeightOffset;

        return new Rect(pCurrentLocation.xMin, newHeightLocation, pCurrentLocation.width, pCurrentLocation.height);
    }

    /// <summary>
    /// Calculate the location of the GUI container so the GUI is perfectly aligned in the center.
    /// </summary>
    /// <param name="pNumber">The number of GUI element in the menu</param>
    /// <param name="pGUIWidth">The width of the largest GUI element</param>
    /// <param name="pGUIHeight">The average height of all the GUI element</param>
    /// <param name="pHeightOffset">The height offset between GUI element</param>
    /// <returns></returns>
    public static Vector2 MenuLocationGUI(float pNumber, float pGUIWidth, float pGUIHeight, float pHeightOffset)
    {
        float xPosition;
        float yPosition;
        float totalOffset;
        float totalGUIHeight;

        xPosition = (Screen.width * 0.5f) - (pGUIWidth / 2);

        totalOffset = (pNumber - 1) * pHeightOffset;
        totalGUIHeight = pNumber * pGUIHeight;

        yPosition = (Screen.height * 0.5f) - ((totalGUIHeight + totalOffset) / 2);

        return new Vector2(xPosition, yPosition);
    }

    /// <summary>
    /// Align a texture2D(center,center) inside the rectangle.
    /// </summary>
    /// <param name="pContainerRect"></param>
    /// <param name="pTexture"></param>
    /// <returns></returns>
    public static Rect CenteredGUI(Rect pContainerRect, float pWidth, float pHeight)
    {
        float leftMargin = pContainerRect.x + (pContainerRect.width / 2) - (pWidth / 2);
        float topMargin = pContainerRect.y + (pContainerRect.height / 2) - (pHeight / 2);

        return new Rect(leftMargin, topMargin, pWidth, pHeight);
    }

    /* Player input */

    /// <summary>
    /// Lock input of all the players
    /// </summary>
    /// <param name="pLock"></param>
    private void SetPlayersInputLock(bool pLock)
    {
        foreach (PlayerInput input in listPlayerInput)
        {
            input.SetLockInput(pLock);
        }
    }

    private void InitPlayersInput()
    {
        // 0 = not active
        foreach (PlayerInput input in listPlayerInput)
        {
            input.SetPlayerController(PlayerInput.Controller.Xbox);
            input.SetPlayerNumber(0);
        }

        listPlayerInput[0].SetPlayerController(PlayerInput.Controller.PC);
        listPlayerInput[0].SetPlayerNumber(25);


    }

    /* Get */
    public bool GetIsDrawing()
    {
        return isDrawing;
    }

    public static GUIStyle GetGUIStyle()
    {
        return style;
    }
}
