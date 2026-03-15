using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveableWall : MonoBehaviour
{
    public float moveSpeed = 1f;
    public InputActionProperty moveAction;
    public Transform rightController;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }



    void Update()
    {
        float value = moveAction.action.ReadValue<float>();
        Debug.Log("Trigger value: " + value);

        if (value > 0.1f)
        {
            MoveWall();
        }
    }



    void MoveWall()
    {
        Vector3 wallForward = transform.forward;
        Vector3 controllerForward = rightController.forward;

        float direction = Vector3.Dot(controllerForward, wallForward);

        transform.position += wallForward * direction * moveSpeed * Time.deltaTime;
    }
}
