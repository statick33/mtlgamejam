using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour 
{
    public FullBodyController fullBodyController;
    public float rotationSpeed = 4.5f;

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
            Quaternion rotation = Quaternion.LookRotation(direction - transform.position);
            rotation.z = 0;
            rotation.x = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
}
