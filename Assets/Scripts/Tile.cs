using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class Tile : MonoBehaviour
{
    public Tile topTile;
    public Tile leftTile;
    public Tile rightTile;
    public Tile botTile;
    public bool occupied;
    public GameObject boat;
    public Material normalMaterial;
    public Material highlightedMaterial;
    


    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
    }

    void OnMouseDown()
    {
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
          foreach (GameObject boat in boats) {
              if (boat.transform.GetComponent<MoveBoat>().selected && boat.transform.GetComponent<MoveBoat>().smallBoat &&
              boat.transform.GetComponent<LifeAndPowerDescription>().placingBuoy)
                boat.transform.GetComponent<LifeAndPowerDescription>().placeBuoy(this.gameObject);
          }
    }

    // Update is called once per frame
    void Update()
    {
        Renderer rend = transform.GetComponent<Renderer>();
        if (rend != null){
            rend.material = normalMaterial;
        }
    }
}
