using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        // transform with a small "t" refers to the current object in the class
        transform.position = player.position + offset;
    }
}
