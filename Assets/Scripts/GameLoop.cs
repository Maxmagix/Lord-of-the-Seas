using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gameplay;
using Cameras;

public class GameLoop : MonoBehaviour
{
    public translateCamera camera;
    public WindMove wind;
    public ChooseBoat boats;
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

    public Camera camP1;
    public Camera camP2;
    public Camera camGame;

    public Text remainingP2;
    public Text remainingP1;

    public bool action;

    private int screen;
    private bool gameIsLaunched;

    // Start is called before the first frame update
    void Start()
    {
        gameIsLaunched = false;
        action = false;
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
        wind = this.transform.GetComponent<WindMove>();
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

    public void ChangeCollisions(bool state)
    {
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in boats) {
            boat.GetComponent<Gameplay.MoveBoat>().SetCollisions(state);
        }
    }

    public void changePowerState()
    {
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in boats) {
            if (boat.transform.GetComponent<MoveBoat>().selected)
                if (!boat.transform.GetComponent<MoveBoat>().smallBoat)
                    boat.transform.GetComponent<LifeAndPowerDescription>().deployed = !boat.transform.GetComponent<LifeAndPowerDescription>().deployed;
                else {
                    boat.transform.GetComponent<LifeAndPowerDescription>().placingBuoy = !boat.transform.GetComponent<LifeAndPowerDescription>().placingBuoy;
                }
        }
    }

    public void SetAction(bool newstate)
    {
        action = newstate;
    }
    
    public void setInGame()
    {
            gameIsLaunched = true;
            GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
            foreach (GameObject boat in boats) {
                boat.transform.GetComponent<MoveBoat>().inGame = true;
        }
    }
    
    public void ChangeScreen()
    {
        if (gameIsLaunched)
            action = true;
        screen++;
        if (screen >= 8)
            screen = 4; 
        this.transform.GetComponent<CannonRange>().setState(false);
        switch(screen) {
            case 0: setActiveThisScreen(interTurn1); break;
            case 1: setActiveThisScreen(placementTurn1); Player1Fog.SetActive(false); Player1Boats.SetActive(true); boats.resetToFull(1); break;
            case 2: setActiveThisScreen(interTurn2); camP1.gameObject.SetActive(false); camP2.gameObject.SetActive(true);Player1Fog.SetActive(true); Player1Boats.SetActive(false); break;
            case 3: setActiveThisScreen(placementTurn2); Player2Fog.SetActive(false); Player2Boats.SetActive(true); boats.resetToFull(2); break;
            case 4: setActiveThisScreen(interTurn1); setInGame(); wind.newDirection(); camP2.gameObject.SetActive(false); camGame.gameObject.SetActive(true); Player2Fog.SetActive(true); Player2Boats.SetActive(false); break;
            case 5: setActiveThisScreen(Turn1); Player1Fog.SetActive(false); Player1Boats.SetActive(true); wind.pushBoats(); Turn1.GetComponent<PlayerTurn>().reset(); ChangeCollisions(true); break;
            case 6: setActiveThisScreen(interTurn2); Player1Fog.SetActive(true); Player1Boats.SetActive(false); break;
            case 7: setActiveThisScreen(Turn2); Player2Fog.SetActive(false); Player2Boats.SetActive(true); wind.pushBoats(); Turn2.GetComponent<PlayerTurn>().reset(); ChangeCollisions(true); break;
        }
    }

    void displayNbRemainingBoats()
    {
        remainingP1.text = "Player 1 has " + Player1Boats.transform.childCount + " boats";
        remainingP2.text = "Player 2 has " + Player2Boats.transform.childCount + " boats";
    }

    void resetGame()
    {
        for (int i = 0; i < Player1Boats.transform.childCount; i++)
            Destroy(Player1Boats.transform.GetChild(i).gameObject);
        for (int i = 0; i < Player2Boats.transform.childCount; i++)
            Destroy(Player2Boats.transform.GetChild(i).gameObject);
        camera.setState(1);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetComponent<CannonRange>().show();
        displayNbRemainingBoats();
        if (gameIsLaunched && (Player1Boats.transform.childCount == 0 || Player2Boats.transform.childCount == 0))
            resetGame();
    }
}
