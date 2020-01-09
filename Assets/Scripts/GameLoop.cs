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

    public GameObject Player1Fog;
    public GameObject Player2Fog;

    public GameObject Player1Boats;
    public GameObject Player2Boats;

    private int screen;

    // Start is called before the first frame update
    void Start()
    {
        screen = 0;
        interTurn1.SetActive(true);
        interTurn2.SetActive(false);
        Turn1.SetActive(false);
        Turn2.SetActive(false);
        placementTurn1.SetActive(false);
        placementTurn2.SetActive(false);
        Player1Fog.SetActive(true);
        Player2Fog.SetActive(true);
        Player1Boats.SetActive(false);
        Player2Boats.SetActive(false);
    }

    private void setActiveThisScreen(GameObject screen) {
        interTurn1.SetActive(false);
        interTurn2.SetActive(false);
        Turn1.SetActive(false);
        Turn2.SetActive(false);
        placementTurn1.SetActive(false);
        placementTurn2.SetActive(false); 
        screen.SetActive(true);
    }

    public void ChangeScreen()
    {
        screen++;
        if (screen >= 8)
            screen = 4; 
        switch(screen) {
            case 0: setActiveThisScreen(interTurn1); break;
            case 1: setActiveThisScreen(placementTurn1); Player1Fog.SetActive(false); Player1Boats.SetActive(true); break;
            case 2: setActiveThisScreen(interTurn2); Player1Fog.SetActive(true); Player1Boats.SetActive(false); break;
            case 3: setActiveThisScreen(placementTurn2); Player2Fog.SetActive(false); Player2Boats.SetActive(true); break;
            case 4: setActiveThisScreen(interTurn1); Player2Fog.SetActive(true); Player2Boats.SetActive(false); break;
            case 5: setActiveThisScreen(Turn1); Player1Fog.SetActive(false); Player1Boats.SetActive(true); Turn1.GetComponent<PlayerTurn>().reset(); break;
            case 6: setActiveThisScreen(interTurn2); Player1Fog.SetActive(true); Player1Boats.SetActive(false); break;
            case 7: setActiveThisScreen(Turn2); Player2Fog.SetActive(false); Player2Boats.SetActive(true); Turn2.GetComponent<PlayerTurn>().reset(); break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
