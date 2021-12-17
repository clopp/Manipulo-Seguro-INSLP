using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] Animator handAnimator;
    [SerializeField] InputActionReference gripAction;
    [SerializeField] InputActionReference pitchAction;
    // Start is called before the first frame update
    void Start()
    {
        gripAction.action.performed += GripAnimation;
        pitchAction.action.performed += PitchAnimation;
    }

    private void PitchAnimation(InputAction.CallbackContext obj) {
        handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    }

    private void GripAnimation(InputAction.CallbackContext obj) {
        handAnimator.SetFloat("Grip", obj.ReadValue<float>());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
