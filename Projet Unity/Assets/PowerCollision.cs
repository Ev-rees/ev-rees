using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision colInfo)
    {
        if(colInfo.collider.tag == "obstacleSpecial")
        {
            Debug.Log("Obstacle spessiul");
            Destroy(colInfo.gameObject);
            Destroy(this.gameObject);
        }
    }
}
