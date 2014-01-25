using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pawn : MonoBehaviour 
{
    public enum PlayerAction
    {
        GrabDrop,Throw
    };

    public enum pawnColor
    {
        Bleu = 0,
        Rouge,
        Jaune,
        Orange,
        Mauve,
        Violet,
    };

    bool lockAction = false;

    //color of the player
    public pawnColor presentColorOfPawn;
    

    List<Vector3> respawnLocation;

    void Awake()
    {

    }

	// Use this for initialization
	void Start () 
    {
        presentColorOfPawn = pawnColor.Bleu;
	}

    // Update is called once per frame
    void Update() 
    {
	}
    
    void OnCollisionEnter(Collision pOther)
    {
  
        /*
        if (pOther.gameObject.tag == "ThrowableObject" && pOther.rigidbody != null)
        {
            pOther.rigidbody.velocity = Vector3.zero;

            if (pOther.gameObject.GetComponent<ThrowableObject>().GetIsDeadly())
            {
                Pawn killedBy = pOther.gameObject.GetComponent<ThrowableObject>().GetThrownBy();

                // Making sure to not give urself point for killing urself
                if (killedBy != this)
                {
                    killedBy.YouKilled();
                }

                Kill();
            }
        */
    }

    public void Action(PlayerAction pAction)
    {
        if (lockAction)
        {
            return;
        }

        switch (pAction)
        {
            case PlayerAction.GrabDrop:
                //throwManager.ActionGrabDrop();
                break;
            case PlayerAction.Throw:
                //throwManager.ActionThrow();
                break;
        }
    }

    public void SetLockAction(bool pLock,bool pDropObject)
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

    private void Respawn()
    {
       
    }

    private Vector3 GetRespawnLocation()
    {
        return new Vector3(0, 0, 0);
    }
}
