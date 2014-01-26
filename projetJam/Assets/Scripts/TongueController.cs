using UnityEngine;
using System.Collections;

public class TongueController : MonoBehaviour
{
    private bool isCurrentlyShooting;
    private bool isColliding = false;
    private bool isRetracting = false;

    public float maxTongueDistance;
    public float tongueSpeed;
    public float tongueSmooth;
    public GameObject fruitSocket;

    public Pawn pawn;

    void Start()
    {
    }

    void Update()
    {
        if(isCurrentlyShooting == false)
        {
            transform.position = pawn.TongueSocket.transform.position;
            if (Input.GetKeyDown("space") && isColliding == false)
            {
                isCurrentlyShooting = true;
                pawn.SetLockAction(true, false);
                rigidbody.drag = 0;
                InvokeRepeating("ShootTongue", 0, tongueSmooth);
            }
        }

        transform.rotation = transform.parent.rotation;
        rigidbody.velocity = Vector3.zero;
    }
    void ShootTongue()
    {
        Vector3 movement = new Vector3(tongueSpeed, 0, tongueSpeed);

        movement = Vector3.Scale(movement, transform.forward);

        transform.position = transform.position + movement;

        //Max distance 
        if ((transform.position - pawn.TongueSocket.gameObject.transform.position).magnitude > maxTongueDistance)
        {
            CancelInvoke("ShootTongue");
            InvokeRepeating("RetractTongue", 0, tongueSmooth);
            isRetracting = true;
        }
    }

    void RetractTongue()
    {
        Vector3 movement = new Vector3(tongueSpeed, 0, tongueSpeed);

        movement = Vector3.Scale(Vector3.Scale(movement, transform.forward),new Vector3(-1, 0, -1));

        transform.position = transform.position + movement;
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Bounce")
        {
            if (isCurrentlyShooting)
            {
                CancelInvoke("ShootTongue");
                InvokeRepeating("RetractTongue", 0, tongueSmooth);
                isRetracting = true;
            }
            isColliding = true;
        }
        else if (target.gameObject.tag == "Strawberry" && isCurrentlyShooting == true)
        {
            target.transform.parent = fruitSocket.transform;
            //print("TEST");
            CancelInvoke("ShootTongue");
            InvokeRepeating("RetractTongue", 0, tongueSmooth);
            isRetracting = true;

        }
        else if (target.gameObject.tag == "LizardHead" && isCurrentlyShooting == true && fruitSocket.transform.childCount > 0)
        {
            //print("AVALE ");
            Destroy(fruitSocket.transform.GetChild(0).gameObject);
        }
        else if (target.gameObject.tag == "LizardBody" && isCurrentlyShooting == true && fruitSocket.transform.childCount > 0)
        {
            print("TEST");
            CancelInvoke("ShootTongue");
            InvokeRepeating("RetractTongue", 0, tongueSmooth);
            isRetracting = true;
        }
        
        print(target.gameObject.tag);

    }

    void OnTriggerEnter(Collider target)
    {
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
        if (target.gameObject.tag == "TongueSocket" && isCurrentlyShooting == true && isRetracting == true)
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
    }

    void OnCollisionExit(Collision target)
    {
        if (target.gameObject.tag == "Bounce")
        {
            isColliding = false;
        }
    }
}
