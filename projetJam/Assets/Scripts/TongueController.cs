using UnityEngine;
using System.Collections;

public class TongueController : MonoBehaviour
{

    private bool isCurrentlyShooting;

    Pawn pawn;

    void Start()
    {
        pawn = transform.parent.parent.GetComponent<Pawn>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && isCurrentlyShooting == false)
        {
            isCurrentlyShooting = true;
            InvokeRepeating("ShootTongue", 0, 0.01f);
            pawn.SetLockAction(true, false);
        }
    }
    void ShootTongue()
    {
       // print("SHOOT");
        Vector3 movement = new Vector3(0, 0, 0.05f);
        transform.position = transform.position + movement;
    }

    void RetractTongue()
    {
      //  print("retract");
        Vector3 movement = new Vector3(0, 0, -0.05f);
        transform.position = transform.position + movement;
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Bounce")
        {
            CancelInvoke("ShootTongue");
            InvokeRepeating("RetractTongue", 0, 0.01f);

        }
        else if (target.gameObject.tag == "Player")
        {
            print("POWWOWOWOWOOWOWOWO");
            Vector3 vect = new Vector3(0, 0, 0);
            CancelInvoke("RetractTongue");
            rigidbody.velocity = vect;


            rigidbody.position = pawn.TongueSocket.transform.position;
            isCurrentlyShooting = false;
            pawn.SetLockAction(false, false);
        }
        else
        {
            print(target.gameObject.tag);
        }

    }
}
