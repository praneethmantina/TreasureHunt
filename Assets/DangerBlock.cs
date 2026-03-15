using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerBlock : MonoBehaviour
{
    public PlayerRespawn respawn;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Danger"))
        {
            respawn.RespawnRoom2();
        }
    }
}

