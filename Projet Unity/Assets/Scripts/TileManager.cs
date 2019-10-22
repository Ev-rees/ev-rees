using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public float spawnZ = -15.0f;
    public float tileLength = 15.0f;
    public int amountTilesOnScreen = 10;
    public List<GameObject> activeTiles;
    public float safeZone = 20.0f;

    private Transform player;



    // Start is called before the first frame update
    private void Start()
    {
        activeTiles = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("player").transform;

        for(int i = 0; i < amountTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.position.z - safeZone > (spawnZ - amountTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[0]) as GameObject;
        go.transform.SetParent(transform);

        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;

        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
