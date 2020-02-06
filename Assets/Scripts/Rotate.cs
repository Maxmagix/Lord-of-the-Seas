using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void rotate(int angle)
    {
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in boats) {
            if (boat.gameObject.active && boat.transform.GetComponent<MoveBoat>().selected) {
                Quaternion rot = boat.gameObject.transform.rotation;
                boat.gameObject.transform.rotation.Set(0 + rot.x, angle + rot.y, 0 + rot.z, 0);
                boat.transform.GetComponent<Hitbox>().rotation += angle;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
