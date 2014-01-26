using UnityEngine;
using System.Collections;

public class TongueController : MonoBehaviour
{
    private bool isCurrentlyShooting;
    private bool isColliding = false;
    private bool isRetracting = false;
    private float tongueElapsedTime = 0;

    public float maxTongueDistance;
    public float tongueSpeed;
    public float tongueSmooth;
    public float tongueTimeOut;
    public GameObject fruitSocket;

    public Pawn pawn;
    public PlayerInput input;
    public GameObject tongueExtensible;

    //Input
    float lastGrabAxisValue = 0;
    float lastThrowAxisValue = 0;

    void Start()
    {
    }

    void Update()
    {
        if (isCurrentlyShooting == false)
        {
            float grabAxisValue = 0;
            float throwAxisValue = 0;

            transform.position = pawn.TongueSocket.transform.position;

            if (input.controller == PlayerInput.Controller.PC)
            {
                if (Input.GetKeyDown("space") && isColliding == false)
                {
                    StartShootTongue();
                }
            }
            else if (input.controller == PlayerInput.Controller.Xbox)
            {
                float axisValue = Input.GetAxis(input.GetInput(PlayerInput.Inputs.GrabRelease2));

                if (axisValue > 0)
                {
                    grabAxisValue = axisValue;
                    throwAxisValue = 0;
                }
                else
                {
                    grabAxisValue = 0;
                    throwAxisValue = Mathf.Abs(axisValue);
                }

                if (throwAxisValue >= 0.1 && lastThrowAxisValue < 0.1)
                {
                    StartShootTongue();
                }

                lastThrowAxisValue = throwAxisValue;
            }
        }

        transform.rotation = transform.parent.rotation;
        rigidbody.velocity = Vector3.zero;
    }

    void StartShootTongue()
    {
        isCurrentlyShooting = true;
        pawn.SetLockAction(true, false);
        rigidbody.drag = 0;
        InvokeRepeating("ShootTongue", 0, tongueSmooth);
        pawn.Action(Pawn.PlayerAction.LaunchTongue);
        tongueElapsedTime = Time.time;
    }

    void ShootTongue()
    {
        Vector3 movement = new Vector3(tongueSpeed, 0, tongueSpeed);

        movement = Vector3.Scale(movement, transform.forward);

        transform.position = transform.position + movement;

        /*movement = Vector3.Scale(movement, transform.forward);
        movement.x = 0;
        movement.z = 0;
        print(movement.y);
        //tongueExtensible.transform.localScale = movement;*/
        print(tongueExtensible.transform.localScale);
        //Max distance 
        if ((transform.position - pawn.TongueSocket.gameObject.transform.position).magnitude > maxTongueDistance || Time.time - tongueElapsedTime > tongueTimeOut)
        {
            StartRetractTongue();
        }
    }

    void StartRetractTongue()
    {
        CancelInvoke("ShootTongue");
        InvokeRepeating("RetractTongue", 0, tongueSmooth);
        isRetracting = true;
    }

    void RetractTongue()
    {
        if (Time.time - tongueElapsedTime < tongueTimeOut)
        {
            Vector3 movement = new Vector3(tongueSpeed, 0, tongueSpeed);

            movement = Vector3.Scale(Vector3.Scale(movement, transform.forward), new Vector3(-1, 0, -1));

            transform.position = transform.position + movement;
        }
        else
        {
            stopRetractTongue();
        }
    }

    void stopRetractTongue()
    {
        Vector3 vect = new Vector3(0, 0, 0);
        transform.position = pawn.TongueSocket.transform.position;
        CancelInvoke("RetractTongue");
        CancelInvoke("ShootTongue");
        rigidbody.velocity = vect;

        isRetracting = false;
        isCurrentlyShooting = false;
        pawn.SetLockAction(false, false);
    }

    void OnCollisionEnter(Collision target)
    {
        //If the tongue is hiting a wall type
        if (target.gameObject.tag == "Bounce")
        {
            if (isCurrentlyShooting)
            {
                StartRetractTongue();
            }
            isColliding = true;
        }
        //If the tongue hits a fruit
        else if ((target.gameObject.tag == "Strawberry" || target.gameObject.tag == "Grape" || target.gameObject.tag == "Orange" || target.gameObject.tag == "Kiwi") && isCurrentlyShooting == true)
        {
            if (fruitSocket.transform.childCount == 0)
            {
                target.transform.position = fruitSocket.transform.position;
                target.transform.parent = fruitSocket.transform;
                StartRetractTongue();
            }
            else
            {
                StartRetractTongue();
            }
        }
        //If the tongue hits another lizard head
        else if (target.gameObject.tag == "LizardHead" && isCurrentlyShooting == true && fruitSocket.transform.childCount > 0)
        {
            Destroy(fruitSocket.transform.GetChild(0).gameObject);
        }
        //Tongue bounces on body
        else if (target.gameObject.tag == "LizardBody" && isCurrentlyShooting == true && fruitSocket.transform.childCount > 0)
        {
            RetractTongue();
        }

        print(target.gameObject.tag);

    }



    void OnTriggerEnter(Collider target)
    {
        //When the tongue comes back
        if (target.gameObject.tag == "TongueSocket" && isCurrentlyShooting == true)
        {
            Vector3 vect = new Vector3(0, 0, 0);
            CancelInvoke("RetractTongue");
            rigidbody.velocity = vect;

            isCurrentlyShooting = false;
            isRetracting = false;
            pawn.SetLockAction(false, false);
        }
    }

    //Retard proofing
    void OnTriggerExit(Collider target)
    {
        //Fix a bug whne the tongue is really close
        if (target.gameObject.tag == "TongueSocket" && isCurrentlyShooting == true && isRetracting == true)
        {
            stopRetractTongue();
        }
    }

    void OnCollisionExit(Collision target)
    {
        //Trigger off thevariable to prevent the player from shooting in the wall
        if (target.gameObject.tag == "Bounce")
        {
            isColliding = false;
        }
    }
}
