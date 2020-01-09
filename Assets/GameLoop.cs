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
            case 1: setActiveThisScreen(placementTurn1); break;
            case 2: setActiveThisScreen(interTurn2); break;
            case 3: setActiveThisScreen(placementTurn2); break;
            case 4: setActiveThisScreen(interTurn1); break;
            case 5: setActiveThisScreen(Turn1); Turn1.GetComponent<PlayerTurn>().reset(); break;
            case 6: setActiveThisScreen(interTurn2); break;
            case 7: setActiveThisScreen(Turn2); Turn2.GetComponent<PlayerTurn>().reset(); break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
