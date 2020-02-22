using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class Hitbox : MonoBehaviour
{
    public GameObject hitboxTile;
    public Vector2 size;
    public bool selected;
    private Vector3 lastPos;
    private List<GameObject> hitboxTiles;
    public int rotation = 0;

    void Start() {
        hitboxTiles = new List<GameObject>();
        lastPos = new Vector3(0,0,0);
    }

    public void setHitboxTiles()
    {
        Vector3 actualPos = this.transform.position;
        foreach (GameObject box in hitboxTiles) {
            Destroy(box);
        }
        hitboxTiles.Clear();
        Vector3 boxpos = this.transform.GetComponent<Gameplay.MoveBoat>().tile.transform.position;
        for (int pos = 0; pos < size.x * size.y; pos += 1) {
            GameObject box = Instantiate(hitboxTile, this.transform.GetChild(0).transform);
            hitboxTiles.Add(box);
            Vector3 s = this.transform.localScale;
            box.transform.localScale = new Vector3(1 / s.x, 0.1f, 1 / s.z);
            box.transform.position = boxpos;
            float x = pos / size.x;
            float y = pos % size.x;
            if (rotation == 90 || rotation == 270 || rotation == -90 || rotation == -270) {
                Debug.Log("Horizontal !");
                x = pos % size.x;
                y = pos / size.x;
            }
            if (x > 0 || y > 0) {
                if (x % 2 == 1) {
                    Tile nextTile = this.transform.GetComponent<Gameplay.MoveBoat>().tile;
                    for (; x > 0; x--)
                        nextTile = nextTile.rightTile;
                    box.transform.position = nextTile.transform.position;
                } else {
                    Tile nextTile = this.transform.GetComponent<Gameplay.MoveBoat>().tile.rightTile;
                    for (; x > 0; x--)
                        nextTile = nextTile.leftTile;
                    box.transform.position = nextTile.transform.position;
                }
                if (y % 2 == 1) {
                    Tile nextTile = this.transform.GetComponent<Gameplay.MoveBoat>().tile;
                    for (; y > 0; y--)
                        nextTile = nextTile.topTile;
                    box.transform.position = nextTile.transform.position;                
                } else {
                    Tile nextTile = this.transform.GetComponent<Gameplay.MoveBoat>().tile.topTile;
                    for (; y > 0; y--)
                        nextTile = nextTile.botTile;
                    box.transform.position = nextTile.transform.position;
                }
            }
            box.transform.position += new Vector3(0, -0.05f, 0);
        }
        lastPos = actualPos;
    }

    void Update() {
        if (!this.gameObject.active || !this.transform.GetComponent<Gameplay.MoveBoat>().tile.gameObject.active)
            return;
        Vector3 actualPos = this.transform.position;
        if (actualPos.x != lastPos.x || actualPos.y != lastPos.y || actualPos.z != lastPos.z) {
            setHitboxTiles();
        }
    }
}
