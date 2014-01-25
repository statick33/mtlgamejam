using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour 
{
    public FullBodyController fullBodyController;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 direction = fullBodyController.direction;
        if (direction != Vector3.zero)
        {
            direction.y = transform.position.y;

            Quaternion rotation = Quaternion.LookRotation(transform.position + direction - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3);
        }
	}
}
