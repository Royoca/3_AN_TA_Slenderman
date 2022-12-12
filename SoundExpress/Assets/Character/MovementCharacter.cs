using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    CharacterController ch_controller;
    public float running_speed_;
    public float base_speed_;
    public float speed_;
    public float stamina_;
    public float speed_to_decrease_stamina;
    public float speed_to_increase_stamina;
    public bool moving_;
    public bool running_;

    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        moving_ = false;
        running_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(move == Vector3.zero)
        {
            moving_ = false;
        }
        else
        {
            moving_ = true;
        }

        ch_controller.Move(move * Time.deltaTime * speed_);


        if (Input.GetKey(KeyCode.LeftShift) && stamina_ > 0)
        {
            speed_ = running_speed_;
            stamina_ -= speed_to_decrease_stamina;
            running_ = true;
        }
        else
        {
            running_ = false;
            stamina_ += speed_to_increase_stamina;
            speed_ = base_speed_;
        }

        if (!Input.GetKey(KeyCode.LeftShift) && stamina_ < 100)
        {
            stamina_ += speed_to_increase_stamina;
            running_ = true;
        }


    }
}
