using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeObstacle : MonoBehaviour
{
    public Transform startPoint;
    public VRLocomotion locomotion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null)
            {
                cc.enabled = false;

                other.transform.position = startPoint.position;
                other.transform.rotation = startPoint.rotation;

                cc.enabled = true;

                locomotion.ResetAllWalls();
            }
        }
    }
}
