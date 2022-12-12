using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player_;
    CharacterController ch_controller_;
    public float mouseSensitivity_;
    float cameraVerticalRotation_;

    void Start()
    {
        Cursor.visible = false;
        ch_controller_ = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity_;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity_;

        cameraVerticalRotation_ -= inputY;
        cameraVerticalRotation_ = Mathf.Clamp(cameraVerticalRotation_, -90.0f, 90.0f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation_;

        //player_.Rotate(Vector3.up * inputX);

    }
}
