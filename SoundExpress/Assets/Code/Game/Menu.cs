using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    BranchingMusic music;

    void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EightPagesMode() {
        SceneManager.LoadScene("ForestLayering");
    }

    public void KillSlenderMode() {
        SceneManager.LoadScene("ForestBranching");
    }

    public void ChangeMusic() {
        if(music.currentBranch == BranchingMusic.Branch.A) {
            music.currentBranch = BranchingMusic.Branch.B;
        } else {
            music.currentBranch = BranchingMusic.Branch.A;
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
