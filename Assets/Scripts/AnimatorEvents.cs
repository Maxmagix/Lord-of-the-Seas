using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public MenuButtonController menuButtonController;
    public bool disableOnce;

    void PlaySound(AudioClip sfx) {
        if (!disableOnce) { // is false
            menuButtonController.audioSource.PlayOneShot(sfx);
        } else {
            disableOnce = false;
        }
    }
}
