using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchingData : MonoBehaviour
{
    [SerializeField]
    Transform nextNode;
    [SerializeField]
    Transform alternativeNode;
    [SerializeField]
    BranchingMusic.Branch branch;

    [HideInInspector]
    public bool played;

    [HideInInspector]
    public AudioSource audio;

    BranchingMusic music;

    void Start() {
        played = false;
        audio = transform.GetComponent<AudioSource>();
        music = FindObjectsOfType<BranchingMusic>()[0];
    }

    void Update() {
        if(played && !audio.isPlaying) {
            BranchingData nextAudio;
            if(music.currentBranch == branch && nextNode != null) {
                nextAudio = nextNode.GetComponent<BranchingData>();
                nextAudio.played = true;
                nextAudio.audio.Play();
                played = false;
            } else if (music.currentBranch != branch && alternativeNode != null) {
                nextAudio = alternativeNode.GetComponent<BranchingData>();
                nextAudio.played = true;
                nextAudio.audio.Play();
                played = false;
            } else if (branch == BranchingMusic.Branch.transition && nextNode != null) {
                nextAudio = nextNode.GetComponent<BranchingData>();
                nextAudio.played = true;
                nextAudio.audio.Play();
                played = false;
            }
        }
    }
}
