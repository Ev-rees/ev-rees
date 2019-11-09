using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using WiimoteApi;
using WiimoteApi.Internal;

public class Player : MonoBehaviour
{
    // Variable déterminant à quel joueur on fait référence
    public int whichPlayer;

    // Variable Rigidbody faisant référence au component du joueur (affecté dans Unity)
    public Rigidbody rb;

    // Variable BoxCollider faisant référence au component du joueur (affecté dans Unity)
    public BoxCollider col;

    public Animator playerAnim;

    // Vitesse du joueur
    public float speedZ = 450.0f;

    // Booléen qui détermine si on peut bouger ou non
    public bool canMove = true;

    // Booléen qui détermine si on est en collision ou non
    public bool isHavingCollision = false;

    public GameObject currentObstacle;

    // Force du saut
    public float jumpForce = 10f;

    private float fallMultiplier = 2.5f;

    // Variable pour déterminer si on touche au sol ou non
    public LayerMask groundLayers;

    // Évènement sur les touches du clavier
    private UnityEvent keyUpEvent = new UnityEvent();

    /*---------------------------------------------------------- */

    // Variable contenant la remote
    //private Wiimote remote;
    // Variable du délai entre les lancés
    //private float timeBetweenShots = 1f;
    // Variable du timestamp
    //float timestamp;

    // Variable contenant le projectile
    public GameObject power;

    private void Start()
    {
        // On empêche le joueur via le rigidbody de faire des rotation
        rb.freezeRotation = true;

        // On ajoute un écouteur sur l'évènement
        keyUpEvent.AddListener(jumpEvent);

        // Initialisation des wii remote
        //InitWiimotes();
    }

    private void FixedUpdate()
    {
        if (isHavingCollision && currentObstacle != null) {
            float posYPlayer = transform.position.y - (col.bounds.size.y / 2);
            float posYObstacle = 0.5f + currentObstacle.GetComponent<BoxCollider>().bounds.size.y;

            Debug.Log("Player : " + posYPlayer);
            Debug.Log("Obstacle : " + posYObstacle);

            if (posYPlayer >= posYObstacle) {

                canMove = true;
                isHavingCollision = false;
            }
        }


        // Si le personnage peut bouger et qu'il n'est pas en collision, il peut avancer
        if (canMove && isHavingCollision == false)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedZ * Time.deltaTime);
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
                ThrowPower();

                // Update du temps
                timestamp = Time.time + timeBetweenShots;
            }

        } while (data > 0);*/
    }

    private void Update() {
        if (!IsGrounded())
        {
            // Simule une augmentation de gravité pour rendre le saut plus rapide
            rb.AddForce(Vector3.down * (jumpForce * 2f));
        }

        if (whichPlayer == 1) {
            if (Input.GetKeyUp("z") && Input.GetKeyUp("x") && keyUpEvent != null && IsGrounded())
            {
                keyUpEvent.Invoke();
            }
        }

        if (whichPlayer == 2) {
            if (Input.GetKeyUp("q") && Input.GetKeyUp("w") && keyUpEvent != null && IsGrounded())
            {
                keyUpEvent.Invoke();
            }
        }
    }

    // POUR LE JUMP
    private void jumpEvent() {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnim.SetTrigger("jump");
    }

    // Vérifie si on est proche du sol ou non
    private bool IsGrounded() {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.size.z * .9f, groundLayers);
    }


    // Détecte quand on entre en collision avec un obstacle
    private void OnCollisionEnter(Collision leCol)
    {
        if (leCol.gameObject.tag == "obstacleSpecial" || leCol.gameObject.tag == "obstacleNormal")
        {
            canMove = false;
            isHavingCollision = true;
            currentObstacle = leCol.gameObject;
        }
    }

    // Détecte quand on quitte une collision avec un obstacle
    private void OnCollisionExit(Collision leCol)
    {
        if (leCol.gameObject.tag == "obstacleSpecial" || leCol.gameObject.tag == "obstacleNormal")
        {
            canMove = true;
            isHavingCollision = false;
        }
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
    /* void FinishedWithWiimotes()
    {
        // Parcoure toutes les wii remotes
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            WiimoteManager.Cleanup(remote);
        }
    }*/

    // Lance le pouvoir du joueur
    /* private void ThrowPower () {
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
    }*/
}
