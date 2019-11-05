using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Script relié au empty PortalActivation
public class PortalActivationTest : MonoBehaviour
{
    // Variables contenant les joueurs (affectés dans Unity)
    public GameObject player1;
    public GameObject player2;

    // Positions de départ en x des joueurs
    private float posXPlayer1;
    private float posXPlayer2;

    private void Start()
    {
        // Afectation des positions en X
        posXPlayer1 = player1.transform.position.x;
        posXPlayer2 = player2.transform.position.x;
    }

    private void FixedUpdate()
    {
        // Si les deux touches sont appuyées
        if (Input.GetKeyDown("a") && Input.GetKeyDown("s"))
        {
            // On échange la place des deux joueurs
            player1.transform.position = new Vector3(posXPlayer2, player1.transform.position.y, player2.transform.position.z);
            player2.transform.position = new Vector3(posXPlayer1, player2.transform.position.y, player1.transform.position.z);
        }

        // Si les deux touches sont relâchées
        else if (Input.GetKeyUp("a") && Input.GetKeyUp("s"))
        {
            // On remet les joueurs à leur place
            player1.transform.position = new Vector3(posXPlayer1, player1.transform.position.y, player1.transform.position.z);
            player2.transform.position = new Vector3(posXPlayer2, player2.transform.position.y, player2.transform.position.z);
        }
    }
}
