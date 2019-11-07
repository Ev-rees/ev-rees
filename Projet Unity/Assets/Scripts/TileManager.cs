using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{

    public GameObject tilePrefab;
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
        GameObject go;
        go = Instantiate(tilePrefab) as GameObject;
        go.transform.SetParent(transform);

        go.transform.position =  new Vector3(player.transform.position.x, go.transform.position.y, spawnZ);
        
        //Vector3.forward * spawnZ;
        spawnZ += tileLength;

        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
