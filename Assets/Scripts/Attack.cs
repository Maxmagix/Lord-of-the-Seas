using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class Attack : MonoBehaviour
{
    public GameObject attackTile;
    public Vector2 size;
    public bool selected;
    private Vector3 lastPos;
    private Vector3 actualPos;
    private List<GameObject> attackTiles;
    public int rotation = 0;

    void Start() {
        attackTiles = new List<GameObject>();
        lastPos = new Vector3(0,0,0);
    }

    public void setAttackTiles()
    {
        foreach (GameObject box in attackTiles) {
            Destroy(box.gameObject);
        }
        attackTiles.Clear();
        Vector3 boxpos = this.transform.GetComponent<Gameplay.MoveBoat>().tile.transform.position;
        for (int pos = 0; pos < size.x * size.y; pos += 1) {
            float x = pos / size.x;
            float y = pos % size.x;
            if (rotation == 90 || rotation == 270 || rotation == -90 || rotation == -270) {
                x = pos % size.x;
                y = pos / size.x;
            }
            if (x > 0 || y > 0) {
            GameObject box = Instantiate(attackTile);
            attackTiles.Add(box);
            Vector3 s = this.transform.localScale;
            //box.transform.localScale = new Vector3(1f / s.x, 0.1f, 1f / s.z);
            box.transform.SetParent(this.transform.GetChild(1).transform);
            box.transform.position = boxpos;
            if (x > 0) {
                if (x % 2 == 1) {
                    box.name = "x+";
                    box.transform.position += new Vector3(x + 1, 0, 0);
                } else {
                    box.name = "x-";
                    box.transform.position -= new Vector3(x, 0, 0);
                }
            }
            if (y > 0) {
                if (y % 2 == 1) {
                    box.name = "y+";
                    box.transform.position += new Vector3(0, 0, y + 1);
                } else {
                    box.name = "y-";
                    box.transform.position -= new Vector3(0, 0, y);
                }
            }
            box.transform.position += new Vector3(0, 0.01f, 0);
            }
        }
        lastPos = actualPos;
    }

    void Update() {
        if (!this.gameObject.active || !this.transform.GetComponent<Gameplay.MoveBoat>().tile.gameObject.active)
            return;
        actualPos = this.transform.GetComponent<Hitbox>().actualPos; 
        rotation = this.transform.GetComponent<Hitbox>().rotation;
        if (actualPos.x != lastPos.x || actualPos.y != lastPos.y || actualPos.z != lastPos.z) {
            setAttackTiles();
        }
    }
}
