using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class Rotate : MonoBehaviour
{
    public bool active;
    public GameLoop game;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void turnAngle(int angle)
    {
        if (active) {
            if (!game.action)
                return;
            game.action = false;
        }
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in boats) {
            if (boat.transform.GetComponent<MoveBoat>().selected) {
                boat.transform.GetComponent<Hitbox>().setOccupiedBoat(false);
                boat.transform.GetComponent<Hitbox>().rotation = (angle + boat.transform.GetComponent<Hitbox>().rotation) % 360;
                if (boat.transform.GetComponent<Hitbox>().rotation < 0)
                    boat.transform.GetComponent<Hitbox>().rotation += 360;
                boat.gameObject.transform.rotation = Quaternion.Euler(0, boat.transform.GetComponent<Hitbox>().rotation, 0);
                boat.transform.GetComponent<Hitbox>().setOccupiedBoat(true);
                boat.transform.GetComponent<Hitbox>().setHitboxTiles();
                boat.transform.GetComponent<Attack>().removeAttackTiles();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
