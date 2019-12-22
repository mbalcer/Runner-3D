using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    public GameObject[] stars;
    private Transform playerTransform;
    private int scaleFruit = 5;
    private float spawnZ = 0.0f;
    private int maxSpawnX = 5, countStarsOnX = 0;
    private int spawnX;
    public int collectStars = 0;

    private List<GameObject> activeStars;

    // Start is called before the first frame update
    void Start()
    {
        activeStars = new List<GameObject>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        spawnX = 0;
        for(int i=0; i<10; i++)
        {
            SpawnStar(spawnX);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z > (spawnZ - 50)) {
            SpawnStar(spawnX);
        }
        if(countStarsOnX >= maxSpawnX)
        {
            int lastSpawnX = spawnX;
            while(lastSpawnX == spawnX)
            {
                spawnX = RandomPositionX();
            }
            countStarsOnX = 0;
        }
        if(activeStars.Count > 12)
        {
            DeleteStar();
        }

    }

    private void SpawnStar(int x)
    {
        GameObject go = Instantiate(stars[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(x, 1, spawnZ);
        spawnZ += 5;
        countStarsOnX++;
        activeStars.Add(go);
    }

    private void DeleteStar()
    {
        Destroy(activeStars[0]);
        activeStars.RemoveAt(0);
    }

    private void DeleteStar(GameObject go)
    {
        Destroy(go);
        activeStars.Remove(go);
    }

    private int RandomPositionX()
    {
        return Random.Range(-2, 2);
    }

    public void CollectStar(GameObject go)
    {
        collectStars++;
        DeleteStar(go);
    }
}
