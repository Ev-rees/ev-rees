using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Player player;

    public void stopSpellCasting()
    {
        Debug.Log("Test");
        player.canMove = true;
        player.rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
    }
}
