﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Gameplay;

public class CannonRange : MonoBehaviour
{
    bool state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setState(bool newstate)
    {
        state = newstate;
    }

    public void changeState()
    {
        if (this.gameObject.transform.GetComponent<GameLoop>().action)
            state = !state;
    }

    public void show()
    {
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
          foreach (GameObject boat in boats) {
              if (state) {
                if (boat.GetComponent<MoveBoat>().selected)
                    boat.GetComponent<Attack>().setAttackTiles();
                else
                    boat.GetComponent<Attack>().removeAttackTiles();
              } else {
                boat.GetComponent<Attack>().removeAttackTiles();
              }
        }
    }

    // Update is called once per frame
    void Update()
    {
        show();
    }
}
