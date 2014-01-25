using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Zone : MonoBehaviour
{

    //Different color a zone can have
    public enum zoneColor
    {
        Bleu = 0,
        Rouge,
        Jaune,
        Orange,
        Mauve,
        Violet,
    };

    //List of zones in the level
    public List<Zone> listOfZones = new List<Zone>();
    //Time the zone will be active
    public float zoneActiveTime;
    //Light over the zone
    public Light lightOfZone;

    //If zone is activated
    private bool bZoneActivated;

    //Present zone color
    public zoneColor presentZoneColor;

    //The number of scoring player in the zone
    private int nbScoringPlayers;

    // Initializtion
    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        presentZoneColor = zoneColor.Bleu;
    }

    // Update is called once per frame
    void Update()
    {
        //Display the number of scoring player
        //Display the active time remaining
    }

    // Activate the zone 
    void zoneActivated()
    {
    }

    // Random Selection of the color of the zone
    void selectColor()
    {
    }

    // Choose randomly the new zone to be activatec
    void activateNewZone(Zone pZoneCantBeActivated)
    {
        //deactivate this zone
        //player feed back zone deactivated
    }

    // When player enters trigger
    void OnTriggerEnter(Collider playerColl)
    {
        //if its a player
        if (playerColl.gameObject.tag == "Player")
        {
            Pawn pawnInZone =  playerColl.gameObject.GetComponent<Pawn>();
           
            //if player is the right color break
            if ((int)pawnInZone.presentColorOfPawn == (int)presentZoneColor)
            {
                Debug.Log("Player is of the right color");
            }
            else
            {
                Debug.Log("Player not of right color");
            }          
        }
    }

    // When the player is in the trigger
    void OnTriggerStay(Collider playerColl)
    {
        //if its a player
        if (playerColl.gameObject.tag == "Player")
        {
            Pawn pawnInZone = playerColl.gameObject.GetComponent<Pawn>();

            //if player is the right color break
            if ((int)pawnInZone.presentColorOfPawn == (int)presentZoneColor)
            {
                Debug.Log("Player Is Gaining PTS");
            }           
        }
    }

    // When the player is exiting the trigger
    void OnTriggerExit(Collider playerColl)
    {
        //Player feed back left zone
        Debug.Log("You have left the zone");
    }
}
