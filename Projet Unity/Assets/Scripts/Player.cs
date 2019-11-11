using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private float posYInit;

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

    // Variable faisant référence au script des Wii remotes
    public WiiRemotes scriptRemotes;

    // Variable la wii remote du joueur
    private Wiimote playerRemote;

    // Variable du délai entre les lancés
    private float timeBetweenShots = 1f;

    // Variable du timestamp
    float timestamp;

    // Variable contenant le projectile
    public GameObject power;

    /*---------------------------------------------------------- */

    private void Start()
    {
        // On empêche le joueur via le rigidbody de faire des rotation
        rb.freezeRotation = true;

        // On ajoute un écouteur sur l'évènement
        keyUpEvent.AddListener(jumpEvent);

        posYInit = 0.5575377f;

        if (scriptRemotes.wiiRemotes.Count <= 0)
        {
            scriptRemotes.InitWiimotes();
        }

        // Assignation des Wii Remotes aux joueurs
        if (scriptRemotes.wiiRemotes.Count >= 2)
        {
                if (whichPlayer == 1)
                {
                    playerRemote = scriptRemotes.wiiRemotes[0];
                    Debug.Log("Player 1 : " + playerRemote);
                }
                else if (whichPlayer == 2)
                {
                    playerRemote = scriptRemotes.wiiRemotes[1];
                    Debug.Log("Player 2 : " + playerRemote);
                }
        }
    }

    private void FixedUpdate()
    {
        if (isHavingCollision && currentObstacle != null) {
            float posYPlayer = transform.position.y - posYInit;
            float posYObstacle = currentObstacle.GetComponent<BoxCollider>().bounds.size.y;

            if (posYPlayer >= posYObstacle) {

                canMove = true;
                isHavingCollision = false;
                playerAnim.SetBool("isIdle", false);
            }
        }


        // Si le personnage peut bouger et qu'il n'est pas en collision, il peut avancer
        if (canMove && isHavingCollision == false)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedZ * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (!IsGrounded())
        {
            // Simule une augmentation de gravité pour rendre le saut plus rapide
            rb.AddForce(Vector3.down * (jumpForce * 2f));
        }

        if (whichPlayer == 1)
        {
            if (Input.GetKeyUp("z") && Input.GetKeyUp("x") && keyUpEvent != null && IsGrounded())
            {
                keyUpEvent.Invoke();
            }
        }

        if (whichPlayer == 2)
        {
            if (Input.GetKeyUp("q") && Input.GetKeyUp("w") && keyUpEvent != null && IsGrounded())
            {
                keyUpEvent.Invoke();
            }
        }

        if (playerRemote != null)
        {
            readPlayerRemoteData();
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

            playerAnim.SetBool("isIdle", true);
        }

        if (leCol.gameObject.tag == "missingTile")
        {
            canMove = false;
            playerAnim.SetBool("isIdle", true);
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        }
    }

    // Détecte quand on quitte une collision avec un obstacle
    private void OnCollisionExit(Collision leCol)
    {
        if (leCol.gameObject.tag == "obstacleSpecial" || leCol.gameObject.tag == "obstacleNormal")
        {
            canMove = true;
            isHavingCollision = false;

            playerAnim.SetBool("isIdle", false);
        }
    }

    private void readPlayerRemoteData()
    {   
        // Variable pour contenir le data de la remote
        int data;

        // Tant qu'il y a des données à lire
        do
        {
            // Lire le data
            data = playerRemote.ReadWiimoteData();

            // Aller chercher les données de l'accélération
            float[] accel = playerRemote.Accel.GetCalibratedAccelData();

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

        } while (data > 0);
    }

    // Lance le pouvoir du joueur
    private void ThrowPower () {
        playerAnim.SetTrigger("spellCast");
        canMove = false;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        // On instantie le pouvoir
        GameObject powerMove = Instantiate(power) as GameObject;

        // On le place à la position du joueur
        powerMove.transform.position = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z+4.0f);

        // On va chercher le rigidbody du pouvoir
        Rigidbody rbPower = powerMove.GetComponent<Rigidbody>();

        // Le pouvoir est projeté vers l'avant
        rbPower.velocity = transform.forward * 40;

        // On détruit le pouvoir après une seconde
        Destroy(powerMove, 1f);
    }
}
