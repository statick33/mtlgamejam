using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitSpawnManager : MonoBehaviour 
{
    public List<GameObject> listFruitPrefab = new List<GameObject>();

    private List<Vector3> listFruitRespawnLocation = new List<Vector3>();

    public int minimumFruit = 3;
    public int maximumFruit = 8;

    public float cooldown = 3.0f;

    private float timeSpan;

    private List<GameObject> listSpawnedFruit = new List<GameObject>();

	// Use this for initialization
	void Start () 
    {
        /* Get respawns location of the new loaded level*/
        GameObject fruitRespawn = GameObject.Find("RespawnFruits"); // During game respawn

        listFruitRespawnLocation.Clear();

        if (fruitRespawn == null)
        {
            return;
        }

        foreach (Transform child in fruitRespawn.transform)
        {
            listFruitRespawnLocation.Add(child.transform.position);
        }

        timeSpan = Time.time + cooldown;

        Debug.Log(listFruitRespawnLocation.Count);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.time > timeSpan)
        {
            if (listSpawnedFruit.Count <= minimumFruit || (listSpawnedFruit.Count > minimumFruit && listSpawnedFruit.Count < maximumFruit))
            {
                GameObject fruit = listFruitPrefab[Random.Range(0, 3)];

                foreach (Vector3 boredCounter in listFruitRespawnLocation)
                {
                    Vector3 possibleFreeLocation = listFruitRespawnLocation[Random.Range(0, listFruitRespawnLocation.Count)];

                    bool foundFree = true;
                    foreach (GameObject fruitSpawned in listSpawnedFruit)
                    {
                        if (possibleFreeLocation == fruitSpawned.transform.position)
                        {
                            foundFree = false;
                            break;                        
                        }
                    }

                    if (foundFree)
                    {
                        GameObject spawnedObj = (GameObject)Instantiate(fruit, possibleFreeLocation, new Quaternion());
                        listSpawnedFruit.Add(spawnedObj);

                        timeSpan = Time.time + cooldown;
                        break;
                    }
                }
            }
        }
	}
}
