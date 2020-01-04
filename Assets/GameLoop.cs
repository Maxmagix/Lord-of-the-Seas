using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    public GameObject placementTurn1;
    public GameObject placementTurn2;

    public GameObject interTurn1;
    public GameObject interTurn2;
    public GameObject Turn1;
    public GameObject Turn2;

    // Start is called before the first frame update
    void Start()
    {
        interTurn1.SetActive(true);
        interTurn2.SetActive(false);
        Turn1.SetActive(false);
        Turn2.SetActive(false);
        placementTurn1.SetActive(false);
        placementTurn2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
