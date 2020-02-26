using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class WindMove : MonoBehaviour
{
    public int direction;
    public GameObject WindArrow1;
    public GameObject WindArrow2;

    // Start is called before the first frame update
    void Start()
    {
        direction = 0;    
    }

    public void newDirection()
    {
        direction = Random.Range(0, 360);
        WindArrow1.transform.rotation = Quaternion.Euler(0, 0, direction);
        WindArrow2.transform.rotation = Quaternion.Euler(0, 0, direction);
    }

    public void pushBoats()
    {
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in boats) {
            if (boat.transform.GetComponent<LifeAndPowerDescription>().deployed) {
                if (direction <= 45 || direction >= 315)
                    if (boat.transform.GetComponent<Hitbox>().rotation == 270)
                        boat.transform.GetComponent<MoveBoat>().moveBoatToTile(boat.transform.GetComponent<MoveBoat>().tile.leftTile, false);
                if (direction <= 135 && direction >= 45 )
                    if (boat.transform.GetComponent<Hitbox>().rotation == 180)
                        boat.transform.GetComponent<MoveBoat>().moveBoatToTile(boat.transform.GetComponent<MoveBoat>().tile.botTile, false);
                if (direction <= 225 && direction >= 135)
                    if (boat.transform.GetComponent<Hitbox>().rotation == 90)
                        boat.transform.GetComponent<MoveBoat>().moveBoatToTile(boat.transform.GetComponent<MoveBoat>().tile.rightTile, false);
                if (direction <= 315 && direction >= 225)
                    if (boat.transform.GetComponent<Hitbox>().rotation == 0)
                        boat.transform.GetComponent<MoveBoat>().moveBoatToTile(boat.transform.GetComponent<MoveBoat>().tile.topTile, false);
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
