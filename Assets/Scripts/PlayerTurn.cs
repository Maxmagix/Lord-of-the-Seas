using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour
{
    public GameLoop gameLoop;
    public GameObject fire;
    public GameObject ropeTimeLeft;
    public GameObject ropeMask;

    public float timeForTurn;
    private float spentTime = 0;

    private Vector3 startPosFire;
    private Vector3 startPosRope;
    public GameObject endPos;
    private Vector3 startPosMask;

    public Slider time;

    public GameObject PauseScreen;

    public bool paused;
    public int nbPlayer;


    void Start()
    {
        paused = false;
        timeForTurn = time.value;
        startPosFire = fire.gameObject.transform.position;
        startPosRope = ropeTimeLeft.gameObject.transform.position;
        startPosMask = ropeMask.gameObject.transform.position;

    }

    public void reset()
    {
        spentTime = 0;
    } 

    public void setPaused(bool state) {
        paused = state;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            paused = !paused;
        }
        if (paused) {
            PauseScreen.SetActive(true);
            PauseScreen.transform.GetChild(0).transform.GetChild(1).transform.GetComponent<Text>().text = "It's Player " + nbPlayer.ToString() + " turn,\nPlayer " + (nbPlayer % 2 + 1) + " don't watch !";
            return;
        } else {
            PauseScreen.SetActive(false);
        }
        spentTime += Time.deltaTime;
        if (spentTime > timeForTurn) {
            gameLoop.ChangeScreen();
        }
        float left = 1 - (spentTime / timeForTurn);
        float diffRope = endPos.gameObject.transform.position.x - (startPosMask.x - 285);
        fire.gameObject.transform.position = new Vector3(startPosMask.x + ((1 - left) * diffRope - 400), startPosMask.y, startPosMask.z);
        ropeMask.gameObject.transform.position = new Vector3(startPosMask.x + ((1 - left) * diffRope), startPosMask.y, startPosMask.z);
        ropeTimeLeft.gameObject.transform.position = new Vector3(startPosRope.x, startPosRope.y, startPosRope.z);
        //fire.gameObject.transform.position;
    }
}