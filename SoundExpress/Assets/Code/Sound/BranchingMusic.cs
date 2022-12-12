using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchingMusic : MonoBehaviour
{
    public enum Branch {
        A,
        B,
        transition
    }
    public Branch currentBranch;


    [SerializeField]
    BranchingData firstNode;

    void Start() {
        StartMusic();
    }

    public void StartMusic() {
        firstNode.played = true;
        firstNode.audio.Play();
    }
}
