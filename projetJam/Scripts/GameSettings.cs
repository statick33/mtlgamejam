using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour 
{

    List<PlayerSettings> listPlayerSettings = new List<PlayerSettings>();

    void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        GameObject.DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () 
    {
        /* Init 4 players */
        for (int i = 0; i < 4; i++)
        {
            PlayerSettings newPlayerSettings = new PlayerSettings();
            listPlayerSettings.Add(newPlayerSettings);
        }
	}

    // Update is called once per frame
    void Update() 
    {
        
	}
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pPlayerSettings"></param>
    /// <param name="pPlayerNumber">Possible values : 1  2  3  4</param>
    public void ChangePlayerSettings(PlayerSettings pPlayerSettings, int pPlayerNumber)
    {
        if (pPlayerNumber > 0 && pPlayerNumber <= listPlayerSettings.Count)
        {
            listPlayerSettings[pPlayerNumber - 1] = pPlayerSettings;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pPlayerNumber">Possible values : 1  2  3  4</param>
    /// <returns></returns>
    public PlayerSettings GetPlayerSettings(int pPlayerNumber)
    {
        if (pPlayerNumber > 0 && pPlayerNumber <= listPlayerSettings.Count)
        {
            return listPlayerSettings[pPlayerNumber - 1];
        }
        else
        {
            return null;
        }
    }

    public List<PlayerSettings> GetAllPlayerSettings()
    {
        return listPlayerSettings;
    }

    public int GetNbPlayerReady()
    {
        int nb = 0;

        for (int i = 0; i < listPlayerSettings.Count; i++)
        {
            if (listPlayerSettings[i].GetReady())
            {
                nb++;
            }
        }

        return nb;
    }

    /// <summary>
    /// Return the index of the next available slot. (-1 = none available)
    /// </summary>
    /// <returns></returns>
    public int GetNextAvailableSlot()
    {
        for (int i = 1; i < listPlayerSettings.Count + 1; i++)
        {
            if (GetPlayerSettings(i).GetControllerNumber() == 0)
            {
                return i;
            }
        }

        return -1;
    }

    public bool GetIsPCPlayerActive()
    {
        for (int i = 1; i < listPlayerSettings.Count + 1; i++)
        {
            if (GetPlayerSettings(i).GetInput() == PlayerInput.Controller.PC)
            {
                return true;
            }
        }

        return false;
    }

    public bool GetIsControllerActive(int pControllerNumber)
    {
        for (int i = 1; i < listPlayerSettings.Count + 1; i++)
        {
            if (GetPlayerSettings(i).GetControllerNumber() == pControllerNumber)
            {
                return true;
            }
        }

        return false;
    }
}
