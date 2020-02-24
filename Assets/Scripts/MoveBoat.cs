using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class MoveBoat : MonoBehaviour
    {
        public Vector2 decal;
        public Tile tile;
        public bool selected;
        private bool collisions;
        public Tile lastTile;

        // Start is called before the first frame update
        void Start()
        {
            collisions = false;
            selected = false;
            Behaviour halo = (Behaviour)GetComponent("Halo");
            halo.enabled = false;
        }

        public void SetCollisions(bool state)
        {
            collisions = state;
        }
        public void SetCollisionsToAll(bool state)
        {
            GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
            foreach (GameObject boat in boats) {
                boat.GetComponent<MoveBoat>().SetCollisions(state);
            }
        }

        public void DeselectAll()
        {
            GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
            foreach (GameObject boat in boats) {
                boat.GetComponent<MoveBoat>().DeselectThisBoat();
            }
        }

        public void DeselectThisBoat()
        {
            selected = true;
            this.OnMouseUp();
        }

        public bool CheckIfEmpty(Tile nextTile)
        {
            GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
            foreach (GameObject boat in boats) {
                if (boat.GetComponent<MoveBoat>().tile == nextTile)
                    return false;
            }
            return true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!selected) return;
            if (Input.GetKeyUp(KeyCode.LeftArrow) && (!collisions || CheckIfEmpty(tile.leftTile)))
            {
                this.transform.position = tile.leftTile.transform.position;
                this.transform.GetComponent<Hitbox>().setOccupiedBoat(false);
                this.transform.position += new Vector3(decal.x, 0, decal.y);
                lastTile = tile;
                tile = tile.leftTile;
                this.transform.GetComponent<Hitbox>().setOccupiedBoat(true);
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) && (!collisions || CheckIfEmpty(tile.topTile)))
            {
                this.transform.position = tile.topTile.transform.position;
                this.transform.GetComponent<Hitbox>().setOccupiedBoat(false);
                this.transform.position += new Vector3(decal.x, 0, decal.y);
                lastTile = tile;
                tile = tile.topTile;
                this.transform.GetComponent<Hitbox>().setOccupiedBoat(true);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) && (!collisions || CheckIfEmpty(tile.rightTile)))
            {
                this.transform.position = tile.rightTile.transform.position;
                this.transform.GetComponent<Hitbox>().setOccupiedBoat(false);
                this.transform.position += new Vector3(decal.x, 0, decal.y);
                lastTile = tile;
                tile = tile.rightTile;
                this.transform.GetComponent<Hitbox>().setOccupiedBoat(true);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && (!collisions || CheckIfEmpty(tile.botTile)))
            {
                this.transform.position = tile.botTile.transform.position;
                this.transform.GetComponent<Hitbox>().setOccupiedBoat(false);
                this.transform.position += new Vector3(decal.x, 0, decal.y);
                lastTile = tile;
                tile = tile.botTile;
                this.transform.GetComponent<Hitbox>().setOccupiedBoat(true);
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
                DeselectAll();
                Behaviour halo = (Behaviour)GetComponent("Halo");
                halo.enabled = true;
                selected = true;
            }
        }
    }
}