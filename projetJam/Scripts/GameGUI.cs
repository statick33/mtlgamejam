using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIManager))]
public abstract class GameGUI : MonoBehaviour 
{
    GUIManager guiManager;


    public virtual void Awake()
    {
        guiManager = GetComponent<GUIManager>();
    }

    void Start()
    {
   
    }

    protected void SwapGUI(GUIManager.GUICommand pCommand)
    {
        guiManager.SwapGUI(pCommand);     
    }

    protected Vector2 MousePosition2D()
    {
        return guiManager.mousePosition2D;
    }
    
}
