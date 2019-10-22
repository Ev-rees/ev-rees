using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManagerP2 : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public float spawnZ = -15.0f;
    public float tileLength = 15.0f;
    public int amountTilesOnScreen = 10;
    public List<GameObject> activeTiles;
    public float safeZone = 20.0f;

    private Transform player2;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log(transform.position.x);

        activeTiles = new List<GameObject>();
        player2 = GameObject.FindGameObjectWithTag("player2").transform;

        for (int i = 0; i < amountTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (player2.position.z - safeZone > (spawnZ - amountTilesOnScreen * tileLength))
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

        go.transform.position = new Vector3(transform.position.x, 0, spawnZ);
        spawnZ += tileLength;

        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
