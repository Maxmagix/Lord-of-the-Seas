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
        public bool smallBoat;
        public bool inGame;

        // Start is called before the first frame update
        void Start()
        {
            inGame = false;
            smallBoat = false;
            collisions = false;
            Behaviour halo = (Behaviour)GetComponent("Halo");
            halo.enabled = false;
            if (this.gameObject.transform.name.Contains("Canope"))
                smallBoat = true;
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
                boat.GetComponent<Attack>().removeAttackTiles();
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

        public void moveBoatToTile(Tile newtile, bool moved_by_arrow)
        {
            if (inGame && moved_by_arrow) {
                if (!smallBoat)
                    return;
                GameObject[] games = GameObject.FindGameObjectsWithTag("Game");
                if (games[0].transform.GetComponent<GameLoop>().action == false)
                    return;
                games[0].transform.GetComponent<GameLoop>().action = false;
                games[0].transform.GetComponent<CannonRange>().setState(false);
            }

            if (newtile.boat != null)
                if (newtile.boat != this.gameObject)
                    return;
            this.transform.GetComponent<Hitbox>().setOccupiedBoat(false);
            this.transform.position = newtile.transform.position;
            this.transform.position += new Vector3(decal.x, 0, decal.y);
            lastTile = tile;
            tile = newtile;
            this.transform.GetComponent<Hitbox>().setHitboxTiles();
            this.transform.GetComponent<Hitbox>().setOccupiedBoat(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (!selected) return;
            if (Input.GetKeyUp(KeyCode.UpArrow) && (!collisions || CheckIfEmpty(tile.leftTile)))
            {
                moveBoatToTile(tile.leftTile, true);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) && (!collisions || CheckIfEmpty(tile.topTile)))
            {
                moveBoatToTile(tile.topTile, true);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && (!collisions || CheckIfEmpty(tile.rightTile)))
            {
                moveBoatToTile(tile.rightTile, true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) && (!collisions || CheckIfEmpty(tile.botTile)))
            {
                moveBoatToTile(tile.botTile, true);
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