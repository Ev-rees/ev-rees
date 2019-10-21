using UnityEngine;
using System.Runtime.Serialization;

public class Player : MonoBehaviour
{
    public Rigidbody rb;

    private float speedZ = 300.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedZ * Time.deltaTime);
    }
}
