using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public GameObject player;
    public float spawnZ = -15.0f;
    public float tileLength = 15.0f;
    public int amountTilesOnScreen = 10;
    public List<GameObject> activeTiles;
    public float safeZone = 20.0f;


    private void Start()
    {
        activeTiles = new List<GameObject>();

        for(int i = 0; i < amountTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    private void Update()
    {
       if (player.transform.position.z - safeZone > (spawnZ - amountTilesOnScreen * tileLength))
       {
            SpawnTile();
            DeleteTile();
       }
    }

    private void SpawnTile()
    {
        GameObject go = null;

        float num = Random.Range(0, 100.0f);

        Debug.Log(num);

        if (num >= 0 && num <= 85.0f)
        {
            go = Instantiate(tilePrefabs[0]) as GameObject;
        }

        if (num > 85.0f && num <= 100.0f && activeTiles.Count > 4)
        {
            go = Instantiate(tilePrefabs[1]) as GameObject;
        }

        go.transform.position =  new Vector3(player.transform.position.x, go.transform.position.y, spawnZ);
        
        spawnZ += tileLength;

        activeTiles.Add(go);
    }

    private void DeleteTile(int index = 0)
    {
        Destroy(activeTiles[index]);
        activeTiles.RemoveAt(index);
    }
}
