using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] AnimatorEvents animatorFunctions;
    [SerializeField] Animator animator;
    [SerializeField] int currIdx;

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.idx == currIdx) {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1) // if button selected
                animator.SetBool("pressed", true);
            else if (animator.GetBool("pressed")) { // if button pressed
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;
            }
        } else {
            animator.SetBool("selected", false);
        }
    }
}
