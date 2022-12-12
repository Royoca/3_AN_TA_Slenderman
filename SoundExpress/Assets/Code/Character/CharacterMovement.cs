using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject pauseMenu_;
    bool pause;
    CharacterController ch_controller;
    public GameObject GameObjectHit;
    public float running_velocity_;
    public float base_velocity_;
    public GameObject cameraForward;
    Vector3 v_movement;
    Vector3 h_movement;
    Vector3 position_sphere_cast;
    float inputX;
    float inputZ;
    public float sphereRadius;
    float currentHitDistance;
    public float maxDistance;
    public float amount_increase_stamina;
    public float amount_decrease_stamina;
    public float stamina_;
    public bool moving_;
    public bool running_;
    public Slider staminaBar;

    public AudioSource layerDrumsSource_;
    public AudioSource layerOtherSource_;
    public AudioSource pauseMenuMusic_;

    SoundManager soundManager_;
    public float sword_speed_;
    public bool isPaused_;

    public bool hasSword_;

    public bool isAttacking_;

    float swordLerpTime_ = 2.0f;
    float crossfadingTime_ = 2.0f;

    public GameObject swordGameObject_;

    Animator anim_;

    void Start()
    {
        isAttacking_ = false;
        ch_controller = GetComponent<CharacterController>();
        anim_ = cameraForward.GetComponent<Animator>();
        stamina_ = 100;
        staminaBar.maxValue = 100;
        staminaBar.value = stamina_;
        pause = (pauseMenu_ != null);
        if (pause)
        {
            isPaused_ = false;
            pauseMenuMusic_.volume = 0.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {

        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        position_sphere_cast = transform.position + ch_controller.center;
        if(moving_ || running_) {
            anim_.enabled = true;
        } else {
            anim_.enabled = false;
        }
        if(running_) {
            anim_.speed = 2.5f;
        } else {
            anim_.speed = 1.0f;
        }

        if(Input.GetKeyDown(KeyCode.P) && pause) {
            if(!isPaused_) {
                pauseMenu_.SetActive(true);
                Time.timeScale = 0.0f;
                //layerDrumsSource_.volume = 0.0f;
                //layerOtherSource_.volume = 0.0f;
                
                isPaused_ = true;

                
            } else if(isPaused_) {
                pauseMenu_.SetActive(false);
                Time.timeScale = 1.0f;
                layerDrumsSource_.volume = 1.0f;
                
                StartCoroutine(Crossfading(pauseMenuMusic_, layerOtherSource_));
                isPaused_ = false;
                

            }
        }

        if (swordGameObject_ != null && !isAttacking_)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                StartCoroutine(SwordLerp(swordGameObject_.GetComponent<Transform>().localPosition, swordGameObject_.GetComponent<Transform>().localPosition + Vector3.left + Vector3.down));
            }
        }

        if(isPaused_ && pause) {
            if (pauseMenuMusic_.volume < 1.0f) {
                pauseMenuMusic_.volume += 0.01f;
            }
            if (layerOtherSource_.volume > 0.0f && layerDrumsSource_.volume > 0.0f) {
                layerDrumsSource_.volume -= 0.01f;
                layerOtherSource_.volume -= 0.01f;
            }
        }
    }

    private void FixedUpdate()
    {
        v_movement = ch_controller.transform.forward * inputZ;
        h_movement = ch_controller.transform.right * inputX;


        if (Input.GetAxis("Horizontal") != 0 && !isPaused_)
        {
            if (stamina_ > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ch_controller.Move(h_movement * running_velocity_ * Time.deltaTime);
                    running_ = true;
                    stamina_ -= amount_decrease_stamina * Time.deltaTime;
                    //moving_ = false;
                }
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                ch_controller.Move(h_movement * base_velocity_ * Time.deltaTime);
                running_ = false;
                //moving_ = false;

            }
            if (stamina_ <= 0 && Input.GetKey(KeyCode.LeftShift))
            {
                ch_controller.Move(h_movement * base_velocity_ * Time.deltaTime);
                running_ = false;
                stamina_ = 0.0f;
            }
        }

        if (Input.GetAxis("Vertical") != 0 && !isPaused_)
        {
            if (stamina_ > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ch_controller.Move(v_movement * running_velocity_ * Time.deltaTime);
                    stamina_ -= amount_decrease_stamina * Time.deltaTime;
                    running_ = true;
                }
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                ch_controller.Move(v_movement * base_velocity_ * Time.deltaTime);
                running_ = false;
           

            }

            if(stamina_ <= 0 && Input.GetKey(KeyCode.LeftShift))
            {
                ch_controller.Move(v_movement * base_velocity_ * Time.deltaTime);
                running_ = false;
                stamina_ = 0.0f;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift) && stamina_ <= 100)
        {
            stamina_ += amount_increase_stamina * Time.deltaTime;
        }
        staminaBar.value = stamina_;

        if (inputX != 0 || inputZ != 0)
            moving_ = true;
        else
            moving_ = false;


        if (running_)
            moving_ = false;

        RaycastHit hit;
        float distanceToObstacle = 0;

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && stamina_ <= 100)
            {
                stamina_ += (amount_increase_stamina * 2) * Time.deltaTime;
            }
        }
            if (Input.GetKey(KeyCode.E))
            {
            int mask = 1 << LayerMask.NameToLayer("note");
            if (Physics.SphereCast(position_sphere_cast, sphereRadius, cameraForward.transform.forward, out hit, maxDistance, mask))
            {
                //distanceToObstacle = hit.distance;
                //Debug.Log(distanceToObstacle);
                GameObjectHit = hit.transform.gameObject;
                currentHitDistance = hit.distance;
                if (hit.transform.CompareTag("note"))
                {
                    Destroy(hit.transform.gameObject);
                    GetComponent<PlayerPages>().takeNotes();
                }
            }
            else
            {
                GameObjectHit = null;
                currentHitDistance = maxDistance;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + cameraForward.transform.forward * currentHitDistance, Color.red);
        Gizmos.DrawSphere(position_sphere_cast + cameraForward.transform.forward * currentHitDistance, sphereRadius);
    }

    public IEnumerator Crossfading(AudioSource source_to_mute, AudioSource source_to_play) {
        
        float accumTime = 0.0f;

        while (accumTime < crossfadingTime_) {
            accumTime += Time.deltaTime;
            float alpha = accumTime/crossfadingTime_;
            
            source_to_mute.volume = Mathf.Lerp(1, 0, alpha);
            source_to_play.volume = Mathf.Lerp(0, 1, alpha);

            yield return null;

        }
    }

    IEnumerator SwordLerp(Vector3 start, Vector3 end) {
        float accumTime = 0.0f;
        //swordGameObject_.GetComponent<BoxCollider>().SetActive(true);
        isAttacking_ = true;
        while (accumTime < swordLerpTime_) {
            accumTime += Time.deltaTime * sword_speed_;
            float alpha = accumTime/swordLerpTime_;

            swordGameObject_.GetComponent<Transform>().localPosition = Vector3.Lerp(start, end, alpha);

            yield return null;
        }
        swordGameObject_.GetComponent<Transform>().localPosition = start;
        isAttacking_ = false;
        //swordGameObject_.GetComponent<BoxCollider>().SetActive(true);
    }
}
