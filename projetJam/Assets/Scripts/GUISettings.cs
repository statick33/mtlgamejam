using UnityEngine;
using System.Collections;

public static class GUISettings 
{
    public static float buttonHeightPadding;
    public static float buttonWidth;
    public static float buttonHeight;
    public static float buttonOffset;

    static GUISettings()
    {
        buttonHeightPadding = 10.0f;
        buttonWidth = GUIManager.ResizeGUI(0.25f, GUIManager.DistanceType.Width);
        buttonHeight = GUIManager.GetGUIStyle().CalcHeight(new GUIContent("Hello World!"), 50.0f) + buttonHeightPadding;
        buttonOffset = 1.0f;
    }
}
