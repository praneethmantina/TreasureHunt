using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRLocomotion : MonoBehaviour
{
    public CharacterController controller;
    public Transform head;
    public Transform rightController;



    public List<Transform> walls = new List<Transform>();

    private List<Vector3> wallOriginalPositions = new List<Vector3>();


    public float groundSpeed = 2f;

    public float airSpeed = 4f;

    public float gravity = -9.8f;

    public float jumpForce = 5f;



    public InputActionProperty moveAction;
    public InputActionProperty jumpAction;
    public InputActionProperty wallMoveAction;
    public InputActionProperty rightJoystickAction;

    private float verticalVelocity;

    private void Start()
    {
        foreach (Transform w in walls)
        {
            wallOriginalPositions.Add(w.position);
        }
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        wallMoveAction.action.Enable();
        rightJoystickAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        wallMoveAction.action.Disable();
        rightJoystickAction.action.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleWallMovement();
    }


    private void HandleMovement()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        Vector3 forward = head.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = head.right;
        right.y = 0;
        right.Normalize();

        float currentSpeed = controller.isGrounded ? groundSpeed : airSpeed;

        Vector3 horizontalVelocity = (forward * input.y + right * input.x) * currentSpeed;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -1f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        if (controller.isGrounded && jumpAction.action.WasPerformedThisFrame())
            verticalVelocity = jumpForce;

        Vector3 finalVelocity = horizontalVelocity;
        finalVelocity.y = verticalVelocity;

        controller.Move(finalVelocity * Time.deltaTime);
    }



    private void HandleWallMovement()
    {
        float triggerValue = wallMoveAction.action.ReadValue<float>();

        if (triggerValue < 0.1f)
            return;

        Ray ray = new Ray(rightController.position, head.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                MoveWallWithRightJoystick(hit.collider.transform);
            }
        }
    }

    private void MoveWallWithRightJoystick(Transform wall)
    {
        Vector2 input = rightJoystickAction.action.ReadValue<Vector2>();

        if (input == Vector2.zero)
            return;

        if (!IsFacingWall(wall))
            return;

        Vector3 right = head.right;
        right.y = 0;
        right.Normalize();
        Vector3 horizontalMove = right * input.x;

        Vector3 verticalMove = Vector3.up * input.y;

        Vector3 moveDir = horizontalMove + verticalMove;

        wall.position += moveDir * Time.deltaTime;
    }

    private bool IsFacingWall(Transform wall)
    {
        Vector3 toWall = (wall.position - head.position).normalized;
        float dot = Vector3.Dot(head.forward, toWall);
        return dot > 0.5f;
    }


    public void ResetAllWalls()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].position = wallOriginalPositions[i];
        }
    }
}