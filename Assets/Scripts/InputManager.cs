using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent TriggerPressedEvent;
    public float fireCooldown;

    private InputDevice controller;
    private bool isTriggerButtonPressed = false;
    private float nextFireTime;

    void Start()
    {
        // https://docs.unity3d.com/Manual/xr_input.html
        var handDevices = new List<InputDevice>();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, handDevices);

        if (handDevices.Count >= 1)
        {
            controller = handDevices[0];
            //cursor.GetComponent<Renderer>().material.color = new Vector4(0, 0, 255, 255);
        }
    }

    void Update()
    {
        // https://sdk.picovr.com/docs/XRPlatformSDK/Unity/en/chapter_five.html#introduction-to-pico-g2-4k-input
        if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out isTriggerButtonPressed) && isTriggerButtonPressed)
        {
            if (Time.time >= nextFireTime)
            {
                TriggerPressedEvent.Invoke();
                nextFireTime = Time.time + fireCooldown;
            }
        }
    }
}
