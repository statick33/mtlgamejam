using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TallyGUI : GameGUI 
{
    Texture2D background;

    Font scoreText;

    GUIStyle style = new GUIStyle();

    List<Pawn> listPawn = new List<Pawn>();
    List<int> listPlayerScore = new List<int>();

    List<Texture2D> listTexturePlayer = new List<Texture2D>();

	// Use this for initialization
	void Start () 
    {
        //inGameGUI = (Texture2D)Resources.Load("ingame_score");

        background = (Texture2D)Resources.Load("bg_clean");

        listTexturePlayer.Add((Texture2D)Resources.Load("tally_player01"));
        listTexturePlayer.Add((Texture2D)Resources.Load("tally_player02"));
        listTexturePlayer.Add((Texture2D)Resources.Load("tally_player03"));
        listTexturePlayer.Add((Texture2D)Resources.Load("tally_player04"));

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
        Rect inGameRect = new Rect(0, 0, GUIManager.ResizeGUI(1.0f, GUIManager.DistanceType.Width) + 1, GUIManager.ResizeGUI(1.0f, GUIManager.DistanceType.Height) + 1);
        GUI.DrawTexture(inGameRect, background);

        for (int i = 0; i < 4; i++)
        {
            
        }
    }
}
