using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordCollider : MonoBehaviour {
    
    public GameObject slendermanGameObject_;
    public CharacterMovement characterMovement_;

    public void OnTriggerEnter(Collider other) {
        if(other == slendermanGameObject_.GetComponent<Collider>()) {
            if(characterMovement_.isAttacking_) {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
