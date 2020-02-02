using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
	public InputField money;
    public InputField time;
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
            if (Int32.TryParse(time.text, out value) && value > 0) {
                confirm.gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = new Color(1, 0.8564f, 0, 1);
                confirm.interactable = true;
            } else {
                confirm.gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = new Color(0.6320f, 0.6320f, 0.6320f, 1);
                confirm.interactable = false;
            }
        } else {
            confirm.gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = new Color(0.6320f, 0.6320f, 0.6320f, 1);
            confirm.interactable = false;
        }
    }
}
