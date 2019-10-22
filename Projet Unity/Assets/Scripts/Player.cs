using UnityEngine;
using System.Threading;
using WiimoteApi;
using WiimoteApi.Internal;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider col;

    public LayerMask groundLayers;
    public float speedZ = 450.0f;
    public float jumpForce = 3f;
    public float distToGround = 0.5f;

    public Wiimote remote;
    private float accel_result;
    float[] pointer;
    float timeBetweenShots = 1f;
    float timestamp;

    public GameObject power;

    void Start()
    {
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        InitWiimotes();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedZ * Time.deltaTime);
         
        if (IsGrounded() && Input.GetKeyUp("space") && Input.GetKeyUp("up"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        int data;
        do
        {
            data = remote.ReadWiimoteData();

            float[] accel = remote.Accel.GetCalibratedAccelData();
            accel_result = accel[0];

            if (accel_result > 2 && Time.time >= timestamp)
            {
                Debug.Log(Time.time);

                GameObject powerMove = Instantiate(power) as GameObject;
                powerMove.transform.position = transform.position + Camera.main.transform.forward * 2;
                Rigidbody rb = powerMove.GetComponent<Rigidbody>();
                rb.velocity = Camera.main.transform.forward * 40;
                Destroy(powerMove, 1f);
                timestamp = Time.time + timeBetweenShots;
            }

        } while (data > 0);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }

    private void InitWiimotes()
    {
        WiimoteManager.FindWiimotes();
        foreach (Wiimote wiiRemote in WiimoteManager.Wiimotes)
        {
            Debug.Log("Wii Remote trouvée !");
            remote = wiiRemote;

            remote.SetupIRCamera(IRDataType.EXTENDED);

        }
    }

    // À appeler à la fin du jeu
    void FinishedWithWiimotes()
    {
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            WiimoteManager.Cleanup(remote);
        }
    }
}
