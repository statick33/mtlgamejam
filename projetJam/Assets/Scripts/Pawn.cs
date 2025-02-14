using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pawn : MonoBehaviour
{
    public GameObject TongueSocket;

    public enum PlayerAction
    {
        LaunchTongue, RetractTongue, RetractTongueFruit
    };

    public enum pawnColor
    {
        Bleu = 0,
        Rouge,
        Vert,
        Orange,
    };

    bool lockAction = false;

    //color of the pawn
    public pawnColor presentColorOfPawn;

    //pawn score
    private int scoreOfPawn = 0;


    List<Vector3> respawnLocation;

    public GameObject headAnimObj;
    public GameObject bodyAnimObj;

    private Animation headAnim;
    private Animation bodyAnim;

    private CharacterController controller;

    private static int lizardId;

    public FullBodyController bodyController;

    Vector3 lastPawnLocation = new Vector3();


    //Color system
    [System.Serializable]
    public class skinProperties
    {
        public Color greenSkinColor;
        public Color orangeSkinColor;
        public Color blueSkinColor;
        public Color redSkinColor;
    }

    //Lizard color properties
    public skinProperties colorSkinProperties = new skinProperties();

    public Texture Texture1;
    public Texture Texture2;
    public Texture Texture3;
    public Texture Texture4;

    void Awake()
    {
        headAnim = headAnimObj.GetComponent<Animation>();
        bodyAnim = bodyAnimObj.GetComponent<Animation>();
        controller = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start()
    {
        SetColor("green");
        SetTexture();
        lizardId++;
        lastPawnLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bodyController.direction != Vector3.zero)
        {
            if (bodyAnim.IsPlaying("BodyWalk") == false)
            {
                bodyAnim.Play("BodyWalk");
                headAnim.Play("HeadWalkNoFruit");
            }
        }
        else
        {
            if (bodyAnim.IsPlaying("BodyWalk"))
            {
                bodyAnim.Stop();
                headAnim.Stop();
            }
        }

        lastPawnLocation = transform.position;

    }

    //getter for score
    public int getScorePawn()
    {
        return scoreOfPawn;
    }

    //setter for score
    public void setScorePawn(int newScore)
    {
        scoreOfPawn = newScore;
    }

    //add pts to score
    public void addPoints(int ptsToAdd)
    {
        scoreOfPawn += ptsToAdd;
    }


    void OnCollisionEnter(Collision pOther)
    {

    }

    public void Action(PlayerAction pAction)
    {
        /*
        if (lockAction)
        {
            return;
        }*/

        switch (pAction)
        {
            case PlayerAction.LaunchTongue:
                LaunchTongue();
                break;
            case PlayerAction.RetractTongue:
                break;
            case PlayerAction.RetractTongueFruit:
                break;
            default:
                break;
        }
    }

    private void LaunchTongue()
    {
        //headAnim["HeadTongueOutNotFruit"].speed = 0.10f;
        //bodyAnim["BodyToungueOutNoFruit"].speed = 0.10f;

        headAnim.Play("HeadTongueOutNotFruit");
        bodyAnim.Play("BodyToungueOutNoFruit");

        Invoke("BackToIdle", 0.9f);
    }

    private void BackToIdle()
    {
        headAnim.Play("HeadIdle");
        bodyAnim.Play("BodyIdle");
    }

    private void RetractTongue()
    {

    }

    private void RetractTongueFruit()
    {

    }

    public void SetLockAction(bool pLock, bool pDropObject)
    {
        if (pLock)
        {
            if (pDropObject)
            {
                //throwManager.RemoveObject();
            }
            lockAction = true;
        }
        else
        {
            lockAction = false;
        }
    }

    public bool GetLockAction()
    {
        return lockAction;
    }

    /* Kill and respawn */
    public void Kill()
    {
        /*
        invincible = true;
        SetLockAction(true,true);
        tlbCamera.Shake();
        Invoke("Respawn", 0.5f);*/
    }

    //Set the lizard color
    public void SetColor(string color)
    {
        Color newColor = new Color();

        switch (color)
        {
            case "blue":
                newColor = colorSkinProperties.blueSkinColor;
                presentColorOfPawn = pawnColor.Bleu;
                break;
            case "green":
                newColor = colorSkinProperties.greenSkinColor;
                presentColorOfPawn = pawnColor.Vert;
                break;
            case "red":
                newColor = colorSkinProperties.redSkinColor;
                presentColorOfPawn = pawnColor.Rouge;
                break;
            case "orange":
                newColor = colorSkinProperties.orangeSkinColor;
                presentColorOfPawn = pawnColor.Orange;
                break;
        }

        transform.GetChild(0).GetChild(0).GetChild(1).renderer.material.color = newColor;
        transform.GetChild(0).GetChild(1).GetChild(0).GetChild(3).renderer.material.color = newColor;
    }

    //Set the lizard texture
    public void SetTexture()
    {

        Material skinMaterial = transform.GetChild(0).GetChild(0).GetChild(1).renderer.material;
        switch (lizardId)
        {
            case 0:
                skinMaterial.mainTexture = Texture1;
                break;
            case 1:
                skinMaterial.mainTexture = Texture2;
                break;
            case 2:
                skinMaterial.mainTexture = Texture3;
                break;
            case 3:
                skinMaterial.mainTexture = Texture4;
                break;
        }
    }

    private void Respawn()
    {

    }

    private Vector3 GetRespawnLocation()
    {
        return new Vector3(0, 0, 0);
    }
}
