using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShootSelect : MonoBehaviour
{

    public GameObject boat;
    public GameObject cannonBallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        boat = null;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "GridTile") {
            boat = col.transform.GetComponent<Tile>().boat;
        }
    }

    void spawnBall() {
        
    }

    void OnMouseDown()
    {
        if (boat == null)
            Debug.Log("Plouf");
        else
            Debug.Log("BOOM");
        GameObject[] games = GameObject.FindGameObjectsWithTag("Game");
        foreach (GameObject game in games) {
            game.transform.GetComponent<GameLoop>().action = false;
            game.transform.GetComponent<CannonRange>().setState(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
