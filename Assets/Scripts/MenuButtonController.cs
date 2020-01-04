using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public int idx = 0;
    public bool keyDown;
    public int maxIdx;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0) {
            if (!keyDown) { // keyDown is false; this way, we disable spamming up and down
                if (Input.GetAxis("Vertical") < 0) { // move cursor down
                    if (idx < maxIdx)
                        idx++;
                    else
                        idx = 0;
                } else if (Input.GetAxis("Vertical") > 0) { // move cursor up
                    if (idx > 0)
                        idx--;
                    else
                        idx = maxIdx;
                }
                keyDown = true;
            }
        } else {
            keyDown = false;
        }
    }
}
