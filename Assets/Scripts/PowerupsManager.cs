using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] powerups;
    public int spawnZ;
    private HeartManager heartManager;
    private Transform playerTransform;
    private int beginSpawnZ = 100;
    private int powerupsLength = 300;
    void Start()
    {
        heartManager = GameObject.Find("HeartManager").GetComponent<HeartManager>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        spawnZ = randomZ();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z + 40 > spawnZ)
        {
            spawnPowerup();
            beginSpawnZ += powerupsLength;
            spawnZ = randomZ();
        }
    }

    private int randomZ()
    {
        return Random.Range(beginSpawnZ, beginSpawnZ+powerupsLength);
    }

    private int randomPowerup()
    {
        if (heartManager.getHealth() == 3)
            return Random.Range(1, powerups.Length);
        else
            return Random.Range(0, powerups.Length);
    }

    private void spawnPowerup()
    {
        int powerupIndex = randomPowerup();
        GameObject go = Instantiate(powerups[powerupIndex]) as GameObject;

        go.transform.SetParent(transform);
        go.transform.position = new Vector3(0, 0.5f, spawnZ);
    }
}
