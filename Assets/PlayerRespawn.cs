using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public CharacterController controller;

    public Transform player;

    public Transform room1StartPoint;
    public Transform room2StartPoint;

    public void RespawnRoom1()
    {
        controller.enabled = false;
        transform.position = room1StartPoint.position;
        transform.rotation = room1StartPoint.rotation;
        controller.enabled = true;

        controller.Move(Vector3.zero);
    }

    public void RespawnRoom2()
    {
        controller.enabled = false;
        transform.position = room2StartPoint.position;
        transform.rotation = room2StartPoint.rotation;
        controller.enabled = true;

        controller.Move(Vector3.zero);
    }

}
