using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision colInfo)
    {

        if (colInfo.collider.tag == "obstacleNormal")
        {
            //Debug.Log("J'ai touché un obstacle !");
        }
    }
}
