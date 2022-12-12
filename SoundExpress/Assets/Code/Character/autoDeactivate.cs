using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDeactivate : MonoBehaviour
{
    float counter;
    public float time_alive_;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0.0f;
        counter += Time.fixedDeltaTime;
        if (counter >= time_alive_)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
