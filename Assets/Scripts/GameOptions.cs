using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
	public Slider money;
    public Slider time;
    public InputField moneyText;
    public InputField timeText;
    public Button confirm;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void setGold(string mode) {
        if (mode == "slider") {
            moneyText.text = money.value.ToString();
        } else {
            money.value = int.Parse(moneyText.text);
        }

    }

    public void setTime(string mode) {
        if (mode == "slider") {
            timeText.text = time.value.ToString();
        } else {
            time.value = int.Parse(timeText.text);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if ((int)money.value > 0) {
            if ((int)time.value > 0) {
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
