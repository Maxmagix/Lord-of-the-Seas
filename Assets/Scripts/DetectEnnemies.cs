using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lightOn;
    public GameObject lightOff;
    public Tile tile;
    public GameObject smallBoat;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (tile.boat != null) {
            lightOn.SetActive(true);
            lightOff.SetActive(false);
            Renderer rend = tile.gameObject.transform.GetComponent<Renderer>();
            if (rend != null){
                rend.material = tile.highlightedMaterial;
            }
        } else {
            lightOn.SetActive(false);
            lightOff.SetActive(true);
            Renderer rend = tile.gameObject.transform.GetComponent<Renderer>();
            if (rend != null){
                rend.material = tile.normalMaterial;
            }
        }
    }
}
