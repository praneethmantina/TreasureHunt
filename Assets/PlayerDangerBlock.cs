using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDangerBlock : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Danger"))
        {
            Room2GameManager.Instance.PlayerHitDanger();
        }
    }
}
