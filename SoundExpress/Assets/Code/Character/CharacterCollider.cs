using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollider : MonoBehaviour {
    
    public GameObject swordPickUp_;
    public GameObject swordInHand_;
    public GameObject lanternInHand_;
    public Slenderman slendermanScript_;
    public CharacterMovement characterMovement_;

    public void OnTriggerEnter(Collider other) {
        if(other == swordPickUp_.GetComponent<Collider>()) {
            swordInHand_.SetActive(true);
            swordPickUp_.SetActive(false);
            swordPickUp_.GetComponent<Menu>().ChangeMusic();
            lanternInHand_.SetActive(false);
            slendermanScript_.runAway = true;
            characterMovement_.hasSword_ = true;
        }
    }
}
