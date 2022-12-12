using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform player_;
    public CharacterController ch_controller;
    public float mouse_sensibility_;
    float cameraVerticalRotation_;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float InputX = Input.GetAxis("Mouse X") * mouse_sensibility_;
        float InputY = Input.GetAxis("Mouse Y") * mouse_sensibility_;

        cameraVerticalRotation_ -= InputY;

        cameraVerticalRotation_ = Mathf.Clamp(cameraVerticalRotation_, -90.0f, 90.0f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation_;

        ch_controller.transform.Rotate(Vector3.up * InputX);
    }
}
