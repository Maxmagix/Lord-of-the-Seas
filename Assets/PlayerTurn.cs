using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour
{
    public GameLoop gameLoop;
    public Image fire;
    public GameObject ropeTimeLeft;
    public GameObject ropeMask;

    public float timeForTurn;
    private float spentTime = 0;

    private Vector3 startPosFire;
    private Vector3 startPosRope;
    private Vector3 startPosMask;


    void Start()
    {
        startPosFire = fire.gameObject.transform.position;
        startPosRope = ropeTimeLeft.gameObject.transform.position;
        startPosMask = ropeMask.gameObject.transform.position;

    }

    public void reset()
    {
        spentTime = 0;
    } 

    // Update is called once per frame
    void Update()
    {
        //fire.gameObject.GetComponent<Renderer>().material.mainTexture.Play();
        spentTime += Time.deltaTime;
        if (spentTime >= timeForTurn) {
            gameLoop.ChangeScreen();
        }
        float left = 1 - (spentTime / timeForTurn);
        fire.gameObject.transform.position = new Vector3(startPosFire.x + (1 - left) * 500, startPosFire.y, startPosFire.z);
        ropeMask.gameObject.transform.position = new Vector3(startPosMask.x + (1 - left) * 500, startPosMask.y, startPosMask.z);
        ropeTimeLeft.gameObject.transform.position = new Vector3(startPosRope.x, startPosRope.y, startPosRope.z);
        //fire.gameObject.transform.position;
    }
}