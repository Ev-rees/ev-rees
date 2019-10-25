using UnityEngine;

// Script relié à Player 1 Camera
public class FollowPlayer : MonoBehaviour
{
    // Référence au joueur
    public Transform player;

    // Variable offset dont les valeurs sont établies dans Unity
    public Vector3 offset;

    private void Update()
    {
        // Fait en sorte que la caméra suit le joueur avec un décalage (offset)
        transform.position = player.position + offset;
    }
}
