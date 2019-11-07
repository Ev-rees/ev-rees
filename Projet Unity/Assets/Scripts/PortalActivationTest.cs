using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Script relié au empty PortalActivation
public class PortalActivationTest : MonoBehaviour
{
    // Variables contenant les joueurs (affectés dans Unity)
    public GameObject player1;
    public GameObject player2;

    private Player p1Script;
    private Player p2Script;

    // Positions de départ en x des joueurs
    private float posXPlayer1;
    private float posXPlayer2;




    private void Start()
    {
        // Afectation des positions en X
        posXPlayer1 = player1.transform.position.x;
        posXPlayer2 = player2.transform.position.x;

        p1Script = player1.GetComponent<Player>();
        p2Script = player2.GetComponent<Player>();
    }

    private void Update()
    {
        float posZPlayer1 = player1.transform.position.z;
        float posZPlayer2 = player2.transform.position.z;

        // Si les deux touches sont appuyées
        if (Input.GetKeyDown("a") && Input.GetKeyDown("s"))
        {
            if (p1Script.isHavingCollision || p2Script.isHavingCollision) {
                if (p1Script.isHavingCollision && p2Script.isHavingCollision) {
                    player1.transform.position = new Vector3(posXPlayer2, player1.transform.position.y, p2Script.currentObstacle.transform.position.z - 4);
                    player2.transform.position = new Vector3(posXPlayer1, player2.transform.position.y, p1Script.currentObstacle.transform.position.z - 4);
                }
                else if(p1Script.isHavingCollision) {
                    player1.transform.position = new Vector3(posXPlayer2, player1.transform.position.y, posZPlayer2);
                    player2.transform.position = new Vector3(posXPlayer1, player2.transform.position.y, p1Script.currentObstacle.transform.position.z - 4);
                }
                else {
                    player1.transform.position = new Vector3(posXPlayer2, player1.transform.position.y, p2Script.currentObstacle.transform.position.z - 4);
                    player2.transform.position = new Vector3(posXPlayer1, player2.transform.position.y, posZPlayer1);
                }

            }
            else {
                player1.transform.position = new Vector3(posXPlayer2, player1.transform.position.y, posZPlayer2);
                player2.transform.position = new Vector3(posXPlayer1, player2.transform.position.y, posZPlayer1);
            }

        }

        // Si les deux touches sont relâchées
        else if (Input.GetKeyUp("a") && Input.GetKeyUp("s"))
        {
            
            // On remet les joueurs à leur place
           if (p1Script.isHavingCollision || p2Script.isHavingCollision) {
                if (p1Script.isHavingCollision && p2Script.isHavingCollision) {
                    player1.transform.position = new Vector3(posXPlayer1, player1.transform.position.y, p2Script.currentObstacle.transform.position.z - 4);
                    player2.transform.position = new Vector3(posXPlayer2, player2.transform.position.y, p1Script.currentObstacle.transform.position.z - 4);
                }
                else if(p1Script.isHavingCollision) {
                    player1.transform.position = new Vector3(posXPlayer1, player1.transform.position.y, posZPlayer2);
                    player2.transform.position = new Vector3(posXPlayer2, player2.transform.position.y, p1Script.currentObstacle.transform.position.z - 4);
                }
                else {
                    player1.transform.position = new Vector3(posXPlayer1, player1.transform.position.y, p2Script.currentObstacle.transform.position.z - 4);
                    player2.transform.position = new Vector3(posXPlayer2, player2.transform.position.y, posZPlayer1);
                }

            }
            else {
                player1.transform.position = new Vector3(posXPlayer1, player1.transform.position.y, posZPlayer2);
                player2.transform.position = new Vector3(posXPlayer2, player2.transform.position.y, posZPlayer1);
            }

        }
    }
}
