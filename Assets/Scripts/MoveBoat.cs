using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
    public Tile tile;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        Behaviour halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected) return;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.transform.position = tile.leftTile.transform.position;
            tile = tile.leftTile;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            this.transform.position = tile.topTile.transform.position;
            tile = tile.topTile;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            this.transform.position = tile.rightTile.transform.position;
            tile = tile.rightTile;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            this.transform.position = tile.botTile.transform.position;
            tile = tile.botTile;
        }
    }

    void OnMouseUp()
    {
        if (selected)
        {
            selected = false;
            Behaviour halo = (Behaviour)GetComponent("Halo");
            halo.enabled = false;
        }
        else
        {
            Behaviour halo = (Behaviour)GetComponent("Halo");
            halo.enabled = true;
            selected = true;
        }
    }
}
