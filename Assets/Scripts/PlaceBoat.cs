using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBoat : MonoBehaviour
{
    public GameObject BoatSelection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoatSelection.SetActive(true);
    }
}
