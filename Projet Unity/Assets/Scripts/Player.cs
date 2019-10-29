using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using WiimoteApi;
using WiimoteApi.Internal;

// Script relié à Player1
public class Player : MonoBehaviour
{
    // Variable Rigidbody faisant référence au component du joueur (affecté dans Unity)
    public Rigidbody rb;

    // Variable BoxCollider faisant référence au component du joueur (affecté dans Unity)
    public BoxCollider col;

    // Vitesse du joueur
    public float speedZ = 450.0f;

    // Force du saut
    public float jumpForce = 7f;

    public LayerMask groundLayers;

    private bool canJump = true;

    // Évènement sur les touches du clavier
    private UnityEvent keyUpEvent = new UnityEvent();

    /*---------------------------------------------------------- */

    // Variable contenant la remote
    //private Wiimote remote;
    // Variable du délai entre les lancés
    private float timeBetweenShots = 1f;
    // Variable du timestamp
    float timestamp;

    // Variable contenant le projectile
    public GameObject power;

    void Start()
    {
        // On empêche le joueur via le rigidbody de faire des rotation
        rb.freezeRotation = true;

        // On ajoute un écouteur sur l'évènement
        keyUpEvent.AddListener(jumpEvent);

        // Initialisation des wii remote
        //InitWiimotes();
    }

    void FixedUpdate()
    {
        // Fait avancer le personnage
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedZ * Time.deltaTime);

        if (Input.GetKeyUp("space") && keyUpEvent != null)
        {
            keyUpEvent.Invoke();
        }

        // Variable pour contenir le data de la remote
        /* int data;
        // Tant qu'il y a des données à lire
        do
        {
            // Lire le data
            data = remote.ReadWiimoteData();

            // Aller chercher les données de l'accélération
            float[] accel = remote.Accel.GetCalibratedAccelData();

            // Prendre le résultat de l'accélération
            float accel_result = accel[0];

            // Si l'accélération est plus haut que 2 et qu'au moins une seconde s'est écoulée
            if (accel_result > 2 && Time.time >= timestamp)
            {
                // On lance le pouvoir
                throwPower();

                // Update du temps
                timestamp = Time.time + timeBetweenShots;
            }

        } while (data > 0);*/
    }


    // POUR LE JUMP
    // Arrêter déplacement quand on fait la collision
    // Repartir le déplacement quand on jump
    // Faire glisser le player vers le sol
    // Bounce (?)
    private void jumpEvent() {
        if (IsGrounded()) {
            Debug.Log("Je saute");

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded() {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.size.z * .9f, groundLayers);
    }

    // Initialise les wii remotes
    /* private void InitWiimotes()
    {
        // Trouve les wiimotes connectées
        WiimoteManager.FindWiimotes();

        // Parcoure toutes les wii remotes
        foreach (Wiimote wiiRemote in WiimoteManager.Wiimotes)
        {
            Debug.Log("Wii Remote trouvée !");

            // Affectation à notre variable
            remote = wiiRemote;

            // Initialisation de la caméra (peut-être à enlever, dépend si on utilise la caméra ou non)
            // Vérifier si l'accélération dépend de ça
            remote.SetupIRCamera(IRDataType.EXTENDED);

        }
    }*/

    // Clean les wii remotes de l'application
    // À appeler à la fin du jeu
    void FinishedWithWiimotes()
    {
        // Parcoure toutes les wii remotes
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            WiimoteManager.Cleanup(remote);
        }
    }

    // Lance le pouvoir du joueur
    private void throwPower () {
        // On instantie le pouvoir
        GameObject powerMove = Instantiate(power) as GameObject;

        // On le place à la position du joueur
        // Placer à la caméra en ce moment ??
        powerMove.transform.position = transform.position + Camera.main.transform.forward * 2;

        // On va chercher le rigidbody du pouvoir
        Rigidbody rb = powerMove.GetComponent<Rigidbody>();

        // Le pouvoir est projeté vers l'avant
        rb.velocity = Camera.main.transform.forward * 40;

        // On détruit le pouvoir après une seconde
        Destroy(powerMove, 1f);
    }
}
