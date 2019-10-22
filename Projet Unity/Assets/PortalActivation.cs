using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActivation : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private float posPlayer1;
    private float posPlayer2;

    private void Start()
    {
        posPlayer1 = player1.transform.position.x;
        posPlayer2 = player2.transform.position.x;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown("a") && Input.GetKeyDown("s"))
        {
            player1.transform.position = new Vector3(posPlayer2, player1.transform.position.y, player2.transform.position.z);
            player2.transform.position = new Vector3(posPlayer1, player2.transform.position.y, player1.transform.position.z);
        }

        if (Input.GetKeyUp("a") && Input.GetKeyUp("s"))
        {
            player1.transform.position = new Vector3(posPlayer1, player1.transform.position.y, player1.transform.position.z);
            player2.transform.position = new Vector3(posPlayer2, player2.transform.position.y, player2.transform.position.z);
        }
    }
}
