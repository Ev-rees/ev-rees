using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstaclesManager : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    public TileManager tileManager;

    private Transform player;
    private int amountObstaclesOnScreen = 4;
    private float distanceMinBetween = 15.0f;
    private float posY;


    private List<GameObject> activeObstacles;


    void Start()
    {
        activeObstacles = new List<GameObject>();

        player = GameObject.FindGameObjectWithTag("player").transform;
        posY = player.position.y;

        SpawnObstacle();
        StartCoroutine(AddNewObstacle());
    }


    void Update()
    {
        if (activeObstacles.Count > 0 && activeObstacles[0].transform.position.z < player.position.z - 5.0f)
        {
            DeleteObstacle();
        }
    }

    IEnumerator AddNewObstacle()
    {
        while (true)
        {
            if (activeObstacles.Count < amountObstaclesOnScreen && activeObstacles.Count > 0 && tileManager.activeTiles.Count > 0)
            {

                float randDistance = Random.Range(0, (tileManager.activeTiles[tileManager.activeTiles.Count - 1].transform.position.z - activeObstacles[activeObstacles.Count - 1].transform.position.z));

                if (randDistance < 0)
                {
                    randDistance *= -1;
                }

                randDistance += distanceMinBetween;

                if ((activeObstacles[activeObstacles.Count - 1].transform.position.z + randDistance) < tileManager.activeTiles[tileManager.activeTiles.Count - 1].transform.position.z)
                {
                    SpawnObstacle(randDistance);
                }
            }

            float randTime = Random.Range(0.5f, 5f);

            yield return new WaitForSeconds(randTime);
        }

    }


    void SpawnObstacle (float randDistance = 30.0f)
    {
        GameObject go;

        int randPrefab = Random.Range(0, obstaclesPrefabs.Length);
        go = Instantiate(obstaclesPrefabs[randPrefab]) as GameObject;
        go.transform.SetParent(transform);


        if (activeObstacles.Count > 0)
        {
            go.transform.position = new Vector3(0, posY, activeObstacles[activeObstacles.Count - 1].transform.position.z + randDistance);
        }
        else
        {
            go.transform.position = new Vector3(0, posY, player.position.z + randDistance);
        }
    

        activeObstacles.Add(go);
    }

    private void DeleteObstacle ()
    {
        Destroy(activeObstacles[0]);
        activeObstacles.RemoveAt(0);
    }
}
