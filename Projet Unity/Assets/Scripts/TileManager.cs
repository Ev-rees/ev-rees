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
        GameObject go;
        GameObject whichTile = tilePrefabs[0];

        float num = Random.Range(0, 100.0f);

        if (num >= 0 && num <= 15 && activeTiles.Count >= 8)
        {
            Debug.Log(num);
            Debug.Log(activeTiles.Count);

            whichTile = tilePrefabs[1];
        }

        go = Instantiate(whichTile) as GameObject;
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
