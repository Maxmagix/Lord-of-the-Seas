using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShootSelect : MonoBehaviour
{
    public GameObject fatherBoat;
    public GameObject dir;   
    public GameObject boat;
    public GameObject cannonBallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        boat = null;
        GameObject[] games = GameObject.FindGameObjectsWithTag("Game");
        foreach (GameObject game in games) {
            dir = game;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "GridTile") {
            boat = col.transform.GetComponent<Tile>().boat;
        }
    }

    void spawnBall() {
        GameObject ball = Instantiate(cannonBallPrefab, dir.transform);
        ball.transform.position = fatherBoat.transform.position;
        fatherBoat.transform.GetComponent<AudioSource>().Play();
        ball.transform.GetComponent<MoveToTile>().startpos = fatherBoat.transform.position;
        ball.transform.GetComponent<MoveToTile>().endpos = this.transform.position;
        ball.transform.GetComponent<MoveToTile>().explodes = (boat == null ? false : true);
        if (boat)
            boat.transform.GetComponent<LifeAndPowerDescription>().life -= 1;
    }

    void OnMouseDown()
    {
        if (dir.transform.GetComponent<GameLoop>().action == false)
            return;
        if (boat == null)
            Debug.Log("Plouf");
        else
            Debug.Log("BOOM");
        spawnBall();
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
