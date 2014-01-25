using UnityEngine;
using System.Collections;

public class LevelSelectGUI : GameGUI 
{
    Texture2D whiteBox;
    Texture2D levelSelector;
    Texture2D officeLevel;

    /* Settings */
    GUIStyle fontStyle = new GUIStyle();
    float fontHeight;

    /* Main container */
    float mainContainerLeftMargin;
    float mainContainerTopMargin;
    float mainContainerWidht;
    float mainContainerHeight;

    float mainContrainerWidthBuffer;
    float mainContainerHeightBuffer;

    Rect mainContainerRect;

    /* Top container (contains arena name) */
    float topContainerWidth;
    float topContainerHeight;

    /* Level gallery container */
    float levelGalleryWidth;
    float levelGalleryHeight;
    float levelGalleryLeftBuffer;
    float levelGalleryTopBuffer;

    /* Mid container (contains level selector) */
    float midContainerWidth;
    float midContainerHeight;

    /* Level picture */
    float levelPictureWidth;
    float levelPictureHeight;

    /* Bot container (contains back button) */
    float botContainerWidth;
    float botContainerHeight;

    /* Back */
    float backTopBuffer;

    /* Level icon (nested inside level gallery container) */
    float levelIconWidth;
    float levelIconHeight;
    float levelIconOffset;

    /* Level selector (nested inside mid container) */
    float levelSelectorWidth;
    float levelSelectorHeight;

	// Use this for initialization
	void Start () 
    {
        whiteBox = (Texture2D)Resources.Load("whiteBox");
        levelSelector = (Texture2D)Resources.Load("levelSelector");
        officeLevel = (Texture2D)Resources.Load("officeLevel");

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

        /* Top container (contains arena name) */
        topContainerWidth = GUIManager.ResizeGUI(0.75f,GUIManager.DistanceType.Width);
        topContainerHeight = GUIManager.ResizeGUI(0.08f, GUIManager.DistanceType.Height);

        /* Level gallery container */
        levelGalleryWidth = GUIManager.ResizeGUI(0.15f, GUIManager.DistanceType.Width);
        levelGalleryHeight = GUIManager.ResizeGUI(0.32f, GUIManager.DistanceType.Height);
        levelGalleryLeftBuffer = GUIManager.ResizeGUI(0.015f, GUIManager.DistanceType.Width);
        levelGalleryTopBuffer = GUIManager.ResizeGUI(0.01f, GUIManager.DistanceType.Height);

        /* Mid container (contains level selector) */
        midContainerWidth = GUIManager.ResizeGUI(0.031f, GUIManager.DistanceType.Width);
        midContainerHeight = GUIManager.ResizeGUI(0.32f, GUIManager.DistanceType.Height);

        /* Level picture */
        levelPictureWidth = GUIManager.ResizeGUI(0.569f,GUIManager.DistanceType.Width);
        levelPictureHeight = GUIManager.ResizeGUI(0.32f,GUIManager.DistanceType.Height);

        /* Bot container (contains back button) */
        botContainerWidth = GUIManager.ResizeGUI(0.75f,GUIManager.DistanceType.Width);
        botContainerHeight = GUIManager.ResizeGUI(0.1f,GUIManager.DistanceType.Height);

        /* Back */
        backTopBuffer = 10;

        /* Level icon (nested inside level gallery container) */
        levelIconWidth = GUIManager.ResizeGUI(0.12f,GUIManager.DistanceType.Width);
        levelIconHeight = GUIManager.ResizeGUI(0.14f, GUIManager.DistanceType.Height);
        levelIconOffset = levelIconHeight + GUIManager.ResizeGUI(0.02f, GUIManager.DistanceType.Height);

        /* Level selector (nested inside mid container) */
        levelSelectorWidth = 25;
        levelSelectorHeight = 50;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwapGUI(GUIManager.GUICommand.Empty);
            Application.LoadLevel("level_02");
            return;
        }
	}

    void OnGUI()
    {
        /* Temp variable */
        string text = "";

        /* Main container */
        GUI.color = Color.white;
        GUI.DrawTexture(mainContainerRect, whiteBox);

        /* Top container (contains arena name) */
        float topContainerLeftMargin;
        float topContainerTopMargin;

        topContainerLeftMargin = mainContainerLeftMargin + mainContrainerWidthBuffer;
        topContainerTopMargin = mainContainerTopMargin + mainContainerHeightBuffer;

        GUI.color = Color.red;
        Rect topContainerRect = new Rect(topContainerLeftMargin, topContainerTopMargin , topContainerWidth,topContainerHeight);
        GUI.DrawTexture(topContainerRect, whiteBox);

        /* Level gallery container */
        float levelGalleryTopMargin;
        levelGalleryTopMargin = topContainerTopMargin + topContainerHeight;

        GUI.color = Color.blue;
        Rect levelGalleryRect = new Rect(topContainerLeftMargin, levelGalleryTopMargin ,levelGalleryWidth,levelGalleryHeight);
        GUI.DrawTexture(levelGalleryRect, whiteBox);

        /* Level icon (nested inside level gallery container) */
        for (int i = 0; i < 2; i++)
        {
            float levelIconLeftMargin;
            float levelIconTopMargin;
            
            levelIconLeftMargin = topContainerLeftMargin + levelGalleryLeftBuffer;
            levelIconTopMargin = levelGalleryTopMargin + levelGalleryTopBuffer + (i * levelIconOffset);

            /* CHANGE COLOR DEPENDING ON STATE (REDO) */
            GUI.color = Color.green;

            if (i == 1)
            {
                GUI.color = Color.grey;
            }


            Rect levelIconRect = new Rect(levelIconLeftMargin, levelIconTopMargin, levelIconWidth, levelIconHeight);
            GUI.DrawTexture(levelIconRect, whiteBox);

            /* Draw level name */
            /* To change with array */

            if (i == 0)
            {
                text = "Office";
            }
            else
            {
                text = "Coming soon!";
            }

            GUI.color = Color.black;
            GUI.Label(GUIManager.CenteredGUI(levelIconRect, fontStyle.CalcSize(new GUIContent(text)).x, fontHeight), text);
        }

        /* Mid container (contains level selector) */
        float midContainerLeftMargin;
        midContainerLeftMargin = topContainerLeftMargin + levelGalleryWidth;

        GUI.color = Color.yellow;
        Rect midContainerRect = new Rect(midContainerLeftMargin, levelGalleryTopMargin, midContainerWidth, midContainerHeight);
        GUI.DrawTexture(midContainerRect, whiteBox);

        /* Level selector (nested inside mid container) */
        GUI.color = Color.white;
        GUI.DrawTexture(GUIManager.CenteredGUI(midContainerRect, levelSelectorWidth, levelSelectorHeight), levelSelector);

        /* Level picture */
        float levelPictureLeftMargin;
        levelPictureLeftMargin = midContainerLeftMargin + midContainerWidth;

        GUI.color = Color.green;
        Rect levelPictureRect = new Rect(levelPictureLeftMargin, levelGalleryTopMargin, levelPictureWidth, levelPictureHeight);
        GUI.DrawTexture(levelPictureRect, whiteBox);

        /* Current arena name */
        float arenaFontHeight;
        GUIStyle arenaNameFontStyle = new GUIStyle();

        GUI.skin.label.fontSize = 25;
        arenaNameFontStyle.fontSize = 25;
        arenaNameFontStyle.fontStyle = FontStyle.Bold;
        arenaFontHeight = arenaNameFontStyle.CalcHeight(new GUIContent("Random"), 1) +7;
        
        text = "Office";

        GUI.color = Color.black;

        Rect arenaNameRect = GUIManager.CenteredGUI(levelPictureRect, arenaNameFontStyle.CalcSize(new GUIContent(text)).x, arenaFontHeight);
        arenaNameRect.y = (topContainerHeight / 2) + topContainerTopMargin - (arenaFontHeight/2);

        GUI.Label(arenaNameRect,text);

        GUI.skin.label.fontSize = 0;

        /* Level office picture (REDO IF MORE LEVEL) */
        GUI.color = Color.white;
        GUI.DrawTexture(levelPictureRect, officeLevel, ScaleMode.StretchToFill);

        /* Bot container (contains back button) */
        float botContainerTopMargin;
        botContainerTopMargin = levelGalleryTopMargin + levelGalleryHeight;

        GUI.color = Color.cyan;
        Rect botContainerRect = new Rect(topContainerLeftMargin, botContainerTopMargin, botContainerWidth, botContainerHeight);
        GUI.DrawTexture(botContainerRect, whiteBox);

        /* Back button */
        float backTopMargin;

        float backFontHeight;
        GUIStyle backFontStyle = new GUIStyle();

        GUI.skin.label.fontSize = 20;
        backFontStyle.fontSize = 20;
        backFontStyle.fontStyle = FontStyle.Bold;
        backFontHeight = backFontStyle.CalcHeight(new GUIContent("Random"), 1) + 7;


        backTopMargin = levelGalleryTopMargin + levelGalleryHeight + backTopBuffer;

        text = "Back";

        Rect backRect = new Rect(topContainerLeftMargin + levelGalleryLeftBuffer, backTopMargin, backFontStyle.CalcSize(new GUIContent(text)).x, backFontHeight);

        GUI.color = Color.black;
        GUI.Label(backRect, text);

        GUI.skin.label.fontSize = 0;
    }
}
