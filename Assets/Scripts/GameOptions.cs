using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
	public InputField money;
    public Button confirm;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var value = 0;

        if (Int32.TryParse(money.text, out value) && value > 0) {
            confirm.interactable = true;
        } else {
            confirm.interactable = false;
        }
    }
}
