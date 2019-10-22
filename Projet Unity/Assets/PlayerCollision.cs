using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public Player movement;

    void OnCollisionEnter(Collision colInfo)
    {

        if(colInfo.collider.tag == "obstacleNormal")
        {
            Debug.Log("J'ai touché un obstacle !");
        }
    }
}
