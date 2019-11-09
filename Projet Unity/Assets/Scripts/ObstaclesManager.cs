using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Script relié à ObstaclesManager
// Génère des obstacles sur la plateforme
public class ObstaclesManager : MonoBehaviour
{
    // Tableau contenant les prefabs des obstacles
    public GameObject[] obstaclesPrefabs;

    // Liste faisant référence au TileManager (affecté dans Unity)
    public TileManager tileManager;

    // Référence au joueur (affecté dans unity)
    public GameObject player;

    // Nombre d'obstacles visibles à la fois
    private int amountObstaclesOnScreen = 4;

    // Distance minimale entre deux éléments
    private float distanceMinBetween = 15.0f;

    // Liste des obstacles présentements sur la plateform
    private List<GameObject> activeObstacles;


    private void Start()
    {
        // Création de la liste
        activeObstacles = new List<GameObject>();

        // Affichage d'un obstacle
        SpawnObstacle();

        // Lancement de la coroutine pour les obstacles
        StartCoroutine(AddNewObstacle());
    }


    private void Update()
    {
        // Si on a au moins un obstacle de présent et qu'il est rendu derrière le joueur
        if (activeObstacles.Count > 0 && activeObstacles[0].transform.position.z < player.transform.position.z - (activeObstacles[0].GetComponent<BoxCollider>().bounds.size.z + 15))
        {
            // On supprime l'obstacle
            DeleteObstacle();
        }
    }

    // Ajoute un obstacle à un temps random
    private IEnumerator AddNewObstacle()
    {
        // À changer éventuellement quand on perd ou gagne, car true le fait à l'infini
        while (true)
        {
            // Si on a moins de 4 et plus de 0 obstacles présents ET il y a plus que 0 tuiles
            if (activeObstacles.Count < amountObstaclesOnScreen && activeObstacles.Count > 0 && tileManager.activeTiles.Count > 0)
            {

                // Génère une distance aléatoire entre 0 et  la différence entre la dernière tuile et le dernier obstacle ajoutés
                float randDistance = Random.Range(0, (tileManager.activeTiles[tileManager.activeTiles.Count - 1].transform.position.z - activeObstacles[activeObstacles.Count - 1].transform.position.z));

                // Si le résultat est négatif, on le remet positif
                if (randDistance < 0)
                {
                    randDistance *= -1;
                }

                // Ajout de la distance minimale
                randDistance += distanceMinBetween;

                // Si la position du dernier obstacle + la distance random est plus petit que la position de la dernière tuile apparue
                if ((activeObstacles[activeObstacles.Count - 1].transform.position.z + randDistance) < tileManager.activeTiles[tileManager.activeTiles.Count - 1].transform.position.z)
                {
                    // Alors on peut ajouter un obstacle
                    SpawnObstacle(randDistance);
                }
            }

            // On génère un temps random
            float randTime = Random.Range(0.5f, 5f);

            // Temps d'attente random
            yield return new WaitForSeconds(randTime);
        }

    }


    // Fait apparaître un obstacle dans le jeu
    private void SpawnObstacle (float randDistance = 30.0f)
    {
        // Variable temporairement d'un obstacle
        GameObject obstacle;

        // On va chercher un obstacle au hasard
        int randPrefab = Random.Range(0, obstaclesPrefabs.Length);

        // On l'instancie et on lui met le manager comme parent
        obstacle = Instantiate(obstaclesPrefabs[randPrefab]) as GameObject;
        obstacle.transform.SetParent(transform);

        // Si on a au moins un obstacle présent, on l'ajoute après le dernier obstacle ajouté
        if (activeObstacles.Count > 0)
        {
            obstacle.transform.position = new Vector3(player.transform.position.x, 0.5f, activeObstacles[activeObstacles.Count - 1].transform.position.z + randDistance);
        }

        // On en  ajoute un après la position du joueur
        else
        {
            obstacle.transform.position = new Vector3(player.transform.position.x, 0.5f, player.transform.position.z + randDistance);
        }

        // Ajout de l'obstacle au jeu
        activeObstacles.Add(obstacle);
    }


    // Détruit le premier obstacle présent dans la liste (le plus vieux)
    private void DeleteObstacle ()
    {
        Destroy(activeObstacles[0]);
        activeObstacles.RemoveAt(0);
    }
}
