using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
    }

    void movePlayer() {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);
    }
}
