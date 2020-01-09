using System.Collections;
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