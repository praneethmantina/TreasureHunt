using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinalBlock : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Final"))
        {
            Room2GameManager.Instance.PlayerReachedFinal();
        }
    }
}
