using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Script relié à Player 1
public class PlayerCollision : MonoBehaviour
{
    // S'exécute lorsque le joueur entre en collision avec un autre éléments
    private void OnCollisionEnter(Collision colInfo)
    {

        if (colInfo.collider.tag == "obstacleNormal")
        {
            //Debug.Log("J'ai touché un obstacle !");
        }
    }
}
