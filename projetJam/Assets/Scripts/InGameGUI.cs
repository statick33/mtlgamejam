using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InGameGUI : GameGUI 
{
    Texture2D inGameGUI;

    

    Font scoreText;

    GUIStyle style = new GUIStyle();

    List<Pawn> listPawn = new List<Pawn>();
    List<int> listPlayerScore = new List<int>();


	// Use this for initialization
	void Start () 
    {
        inGameGUI = (Texture2D)Resources.Load("ingame_score");
        scoreText = (Font)Resources.Load("Woodstamp");
        style.font = scoreText;
        style.fontSize = 34;

        Invoke("FindPlayers", 3);

        listPlayerScore.Add(0);
        listPlayerScore.Add(0);
        listPlayerScore.Add(0);
        listPlayerScore.Add(0);
	}

    private void FindPlayers()
    {
        GameObject[] arrayObj = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in arrayObj)
        {
            listPawn.Add(obj.GetComponent<Pawn>());
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i < listPawn.Count; i++)
        {
            listPlayerScore[i] = listPawn[i].getScorePawn();
        }
	}

    void OnGUI()
    {
        Rect inGameRect = new Rect(GUIManager.ResizeGUI(0.08f, GUIManager.DistanceType.Width), 0, GUIManager.ResizeGUI(0.84f, GUIManager.DistanceType.Width) + 1, GUIManager.ResizeGUI(0.1f, GUIManager.DistanceType.Height) + 1);
        GUI.DrawTexture(inGameRect, inGameGUI);

        Rect textRect = new Rect(GUIManager.ResizeGUI(0.18f, GUIManager.DistanceType.Width), GUIManager.ResizeGUI(0.026f, GUIManager.DistanceType.Height), 30, 30);
        style.normal.textColor = new Color(0.098f, 0.185f, 0.06f);
        GUI.Label(textRect, listPlayerScore[0].ToString(), style);

        textRect = new Rect(GUIManager.ResizeGUI(0.325f, GUIManager.DistanceType.Width), GUIManager.ResizeGUI(0.026f, GUIManager.DistanceType.Height), 30, 30);
        style.normal.textColor = new Color(0.015f, 0.20f, 0.26f);
        GUI.Label(textRect, listPlayerScore[1].ToString(), style);

        textRect = new Rect(GUIManager.ResizeGUI(0.65f, GUIManager.DistanceType.Width), GUIManager.ResizeGUI(0.026f, GUIManager.DistanceType.Height), 30, 30);
        style.normal.textColor = new Color(0.47f, 0.12f, 0.0f);
        GUI.Label(textRect, listPlayerScore[2].ToString(), style);

        textRect = new Rect(GUIManager.ResizeGUI(0.80f, GUIManager.DistanceType.Width), GUIManager.ResizeGUI(0.026f, GUIManager.DistanceType.Height), 30, 30);
        style.normal.textColor = new Color(0.27f, 0.06f, 0.26f);
        GUI.Label(textRect, listPlayerScore[3].ToString(), style);
        
    }
}
