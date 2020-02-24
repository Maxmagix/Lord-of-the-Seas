using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class checkCollision : MonoBehaviour
{
    public GameObject boat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Tile") {
            if (!boat.transform.GetComponent<Gameplay.MoveBoat>().selected)
                return;
            if (boat.transform.GetComponent<MoveBoat>().lastTile == null) {
                boat.transform.GetComponent<Gameplay.MoveBoat>().tile = boat.transform.GetComponent<MoveBoat>().tile.leftTile;
            } else {
                boat.transform.GetComponent<Gameplay.MoveBoat>().tile = boat.transform.GetComponent<MoveBoat>().lastTile;
            }
            boat.transform.position = boat.transform.GetComponent<Gameplay.MoveBoat>().tile.gameObject.transform.position; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
