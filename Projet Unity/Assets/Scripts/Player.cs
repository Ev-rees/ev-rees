using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider col;

    public LayerMask groundLayers;
    public float speedZ = 450.0f;
    public float jumpForce = 7f;
    public float distToGround = 0.5f;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedZ * Time.deltaTime);

        Debug.Log(IsGrounded());

        if(IsGrounded() && Input.GetKeyUp("space") && Input.GetKeyUp("up"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }
}
