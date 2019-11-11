using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision colInfo)
    {
        if(colInfo.collider.tag == "obstacleSpecial")
        {
            Destroy(colInfo.gameObject);
            Destroy(this.gameObject);
        }
    }
}
