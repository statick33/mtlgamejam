using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Zone : MonoBehaviour
{
    public static float timeOfRandomizedLoop = 120;
    //Different color a zone can have
    public enum zoneColor
    {
        Bleu = 0,
        Rouge,
        Vert,
        Orange,
    };
    //between sequences active time
    private  float randomizedActivetimeEffect = 10;
    //In randommize sequence
    private bool bIsSelectedInRandom = false;

    //sound of zone start
    public AudioSource soundOfTheZoneStart;
    //sound of zone end
    public AudioSource soundOfTheZoneEnd;
    
    
    //List of zones in the level
    public List<Zone> listOfZones = new List<Zone>();  
    //Time the zone will be active
    public float zoneActiveTime = 15;
    //Time remaining 
    private float timeRemaing = 0;   
    //Adjustement for point system
    public int vitesseGainPtsSolo = 0;
    //pts the pawn make for being in the zone
    private int ptsForScoringPlayer;
    //Light over the zone
    public Light lightOfZone;
    //the first zone to be light
    public bool bFirstZone = false;
    //If zone is activated
    private bool bZoneActivated;

    //Present zone color
    public zoneColor presentZoneColor;

    //colors of the zone
    
    [System.Serializable]
    public class lightProperties
    {
       
        public Color lightColor = Color.white;
        public int lightIntensity = 10;   
    }

    //light color properties
    public lightProperties greenLightProperties = new lightProperties(); 
    public lightProperties blueLightProperties = new lightProperties();
    public lightProperties redLightProperties = new lightProperties();
    public lightProperties orangeLightProperties = new lightProperties();

   

    //The number of scoring player in the zone
    private int nbScoringPlayers;
///////////////////////////////////////////////////////////////////////////////
    // Initializtion
    void Awake()
    {

    }
///////////////////////////////////////////////////////////////////////////////
    // Use this for initialization
    void Start()
    {
        presentZoneColor = zoneColor.Bleu;
        nbScoringPlayers = 0;
    }
///////////////////////////////////////////////////////////////////////////////
    // Update is called once per frame
    void Update()
    {

        if (bFirstZone)
        {
            bFirstZone = false;           
            zoneActivated(0, true);            
        }

        if (timeOfRandomizedLoop <= 0 && bIsSelectedInRandom)
        {
            timeOfRandomizedLoop = 120;
            bIsSelectedInRandom = false;
            soundOfTheZoneEnd.Play();
            activateNewZone(false);
        }

        if (bZoneActivated )
        {
            //set the pts made by players
            setPtsForThisFrame();
            //Display the number of scoring player MAYBE

            //Decrease time remaining
            timeRemaing -= 1;
            // No time remainning 
            if (timeRemaing == 0)
            {
                activateNewZone(true);
            }
        }

        if (bIsSelectedInRandom)
        {
            //reduce time of random loop
            timeOfRandomizedLoop--;
            //Decrease time remaining
            timeRemaing -= 1;            

            // No time remainning 
            if (timeRemaing == 0)
            {
                activateNewZone(true);
            }
        }
    }
///////////////////////////////////////////////////////////////////////////////
    // Activate the zone 
    public void zoneActivated(int previousZoneColor, bool pRandom)
    {



        if (!pRandom)
        {
            //activate the zone
            bZoneActivated = true;
            //set the remaining time
            timeRemaing = zoneActiveTime * 60;
            // Play sound start
            soundOfTheZoneStart.Play();
        }
        else
        {
            //Is random owner
            bIsSelectedInRandom = true;
            //remainning time for random
            timeRemaing = randomizedActivetimeEffect;
        }

        //set the color of the light
        selectColor(previousZoneColor);
      
    }
///////////////////////////////////////////////////////////////////////////////
    // Random Selection of the color of the zone
    void selectColor(int previousZoneColor)
    {
       int newColor = 0 ;

        //random make sure its not the same as the last one
       do
       {
           newColor = Random.Range(0, 4);
       } while (newColor == previousZoneColor);

        //set the color & applly it to the light
       switch(newColor)
       {
               
           case 0:
               presentZoneColor = zoneColor.Bleu;
               lightOfZone.intensity = blueLightProperties.lightIntensity;
               lightOfZone.color = blueLightProperties.lightColor;
               break;
           case 1:
               presentZoneColor = zoneColor.Rouge;
               lightOfZone.intensity = redLightProperties.lightIntensity;
               lightOfZone.color = redLightProperties.lightColor;
               break;
           case 2:
               presentZoneColor = zoneColor.Vert;
               lightOfZone.intensity = greenLightProperties.lightIntensity;
               lightOfZone.color = greenLightProperties.lightColor;
               break;
           case 3:
               presentZoneColor = zoneColor.Orange;
               lightOfZone.intensity = orangeLightProperties.lightIntensity;
               lightOfZone.color = orangeLightProperties.lightColor;
               break;           
       }

        //activate the light
       lightOfZone.enabled = true;
    }
///////////////////////////////////////////////////////////////////////////////
    // Set the pts that the player will make this frame
    void setPtsForThisFrame()
    {
        if (nbScoringPlayers != 0 && nbScoringPlayers > 1)
        {
            ptsForScoringPlayer = vitesseGainPtsSolo / nbScoringPlayers;
        }
       
    }
///////////////////////////////////////////////////////////////////////////////
    // Choose randomly the new zone to be activatec
    void activateNewZone(bool pRandom)
    {
        bIsSelectedInRandom = false;
        //number of zone in the level
        int nbOfZone = listOfZones.Count;
        //select the the zone to activate randomly
        listOfZones[Random.Range(0, nbOfZone)].zoneActivated((int)presentZoneColor, pRandom);
     
        //deactivate this zone
        bZoneActivated = false;
        
        //player feed back zone deactivated
        lightOfZone.enabled = false;
    }
///////////////////////////////////////////////////////////////////////////////
    // When player enters trigger
    void OnTriggerEnter(Collider playerColl)
    {
        //if its a player
        if (playerColl.gameObject.tag == "Player" /*&& bZoneActivated*/)
        {
            Pawn pawnInZone =  playerColl.gameObject.GetComponent<Pawn>();
           
            //if player is the right color break
            if ((int)pawnInZone.presentColorOfPawn == (int)presentZoneColor)
            {
                //increase the number of player in the zone
                nbScoringPlayers++;
                Debug.Log("Player is of the right color");
            }
            else
            {
                Debug.Log("Player not of right color");
            }          
        }
    }
///////////////////////////////////////////////////////////////////////////////
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
                //increade the pts of the player
                pawnInZone.addPoints(ptsForScoringPlayer);
                Debug.Log("Player Is Gaining PTS");
            }           
        }
    }
///////////////////////////////////////////////////////////////////////////////
    // When the player is exiting the trigger
    void OnTriggerExit(Collider playerColl)
    {
        //Player feed back left zone
        Debug.Log("You have left the zone");
        //Decrease the number of scoring player in the zone
        nbScoringPlayers--;
    }
}
