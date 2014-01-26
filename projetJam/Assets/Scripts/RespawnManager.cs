using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RespawnManager : MonoBehaviour 
{
    public GameObject playerPrefab;

    GameSettings gameSettings;

    List<Pawn> listPlayer = new List<Pawn>();

    List<Vector3> listRespawnLocation = new List<Vector3>();

    void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        GameObject.DontDestroyOnLoad(this);

        GameObject gameSettingsObj = GameObject.Find("Settings");

        if (gameSettingsObj != null)
        {
            gameSettings = GameObject.Find("Settings").GetComponent<GameSettings>();
        }
    }

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnLevelWasLoaded (int pLevel) 
    {
        /* Get respawns location of the new loaded level*/
        GameObject ldRespawn = GameObject.Find("LD_Respawn"); // During game respawn

        listRespawnLocation.Clear();

        if (ldRespawn == null)
        {
            return;
        }

        foreach (Transform child in ldRespawn.transform)
        {
            listRespawnLocation.Add(child.transform.position);
        }

        listPlayer = new List<Pawn>();

        /* Spawn player in their location */
        List<PlayerSettings> listPlayerSettings = gameSettings.GetAllPlayerSettings();

        int currentPlayerRespawning = 0;

        for (int i = 0; i < listPlayerSettings.Count; i++)
        {
            if (listPlayerSettings[i].GetControllerNumber() != 0)
            {
                if (playerPrefab != null)
                {
                    Vector3 playerSpawnLocation = listRespawnLocation[currentPlayerRespawning];
                    //playerSpawnLocation.y = 0.5f;

                    GameObject player = Instantiate(playerPrefab, playerSpawnLocation, new Quaternion()) as GameObject;

                    PlayerInput playerInput = player.GetComponent<PlayerInput>();

                    playerInput.SetPlayerNumber(listPlayerSettings[i].GetControllerNumber());
                    playerInput.SetPlayerController(listPlayerSettings[i].GetInput());

                    listPlayer.Add(player.GetComponent<Pawn>());
                    player.GetComponent<Pawn>().transform.position = playerSpawnLocation;
                    currentPlayerRespawning++;
                }
            }
        }
	}

    public Vector3 GetRespawnLocation(Pawn pPlayer)
    {
        /*
        Vector3 safestRespawnLocation = new Vector3();
        float accumulatedDistance;
       

        foreach (Pawn player in listPlayer)
        {
            
        }
        */ 

        Vector3 location = listRespawnLocation[Random.Range(0,listRespawnLocation.Count)];

        if (location == Vector3.zero)
        {
            Debug.LogError("RespawnManager - GetRespawnLocation - location null");
        }

        return location;
    }


}
