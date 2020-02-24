using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile topTile;
    public Tile leftTile;
    public Tile rightTile;
    public Tile botTile;
    public bool occupied;
    public GameObject boat;


    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
