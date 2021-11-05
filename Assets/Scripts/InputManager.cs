using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent TriggerPressedEvent;
    [SerializeField]
    private float fireCooldown;

    private InputDevice controller;
    private bool isTriggerButtonPressed = false;
    private float nextFireTime;

    void Start()
    {
        // https://docs.unity3d.com/Manual/xr_input.html
        var handDevices = new List<InputDevice>();
        // Pico controller is a HeldInHan Controller
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, handDevices);

        if (handDevices.Count >= 1)
        {
            controller = handDevices[0]; // Pico controller is the first of the list (normally, the only one)
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
