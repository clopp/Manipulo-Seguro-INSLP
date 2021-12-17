using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class AvatarManager : MonoBehaviour {
    public Animator animator;

    private bool onStay;
    private bool doorActive;

    // Start is called before the first frame update
    private void Start() {
        GetComponent<Animator>();

    }

    private void Update() {
        if (onStay)
        {
            animator.SetBool("puertaActiva", true);
        }
        else
            animator.SetBool("puertaActiva", false);




    }
    private void OnTriggerStay(Collider other) {
            
        if (other.tag == "Player"){
            onStay = true;

        }

    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            onStay = false;


        }
    }
}