using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerPages : MonoBehaviour
{

    public int maxPages;
    public int currentPages;
    public List<AudioMixerSnapshot> snapshots;
    public TMP_Text text;
    private void Start()
    {

    }
    public void takeNotes()
    {
        currentPages++;
        text.text = currentPages.ToString();
    }
    public void Update()
    {
        if (maxPages > 0)
        {
            if (currentPages <= 2)
            {
                snapshots[0].TransitionTo(1);
            }
            if (currentPages >= 3 && currentPages < 5)
            {
                snapshots[1].TransitionTo(2);
            }
            if (currentPages >= 5 && currentPages < 7)
            {
                snapshots[2].TransitionTo(3);
            }
            if (currentPages >= 7)
            {
                snapshots[3].TransitionTo(4);
            }
            if (currentPages >= 8) {
                SceneManager.LoadScene("Menu");
            }
        }
        
    }

}
