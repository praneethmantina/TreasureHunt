using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightHandSprintBoost : MonoBehaviour
{
    public VRLocomotion locomotion;

    public float sprintSpeed = 6f;

    public float velocityThreshold = 1.2f;

    private float normalSpeed;

    private InputDevice rightHand;

    void Start()
    {
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        normalSpeed = locomotion.groundSpeed;
    }



    void Update()
    {
        Vector3 rightVel = Vector3.zero;
        rightHand.TryGetFeatureValue(CommonUsages.deviceVelocity, out rightVel);

        bool sprinting = rightVel.magnitude > velocityThreshold;
        locomotion.groundSpeed = sprinting ? sprintSpeed : normalSpeed;
    }
}

