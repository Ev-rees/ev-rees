using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    private Transform player;
    private float spawnZ = -15.0f;
    private float tileLength = 15.0f;
    private float safeZone = 20.0f;

    private int amountTilesOnScreen = 5;

    private List<GameObject> activeTiles;


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

        Debug.Log(Vector3.forward);

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
