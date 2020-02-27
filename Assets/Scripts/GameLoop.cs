using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gameplay;
using Cameras;

public class GameLoop : MonoBehaviour
{
    public bool resetEndGame;
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

    public GameObject endGame;

    public GameObject UIP1;
    public GameObject UIP2;

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
        newGame();
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
        if (!action)
            return;
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in boats) {
            if (boat.transform.GetComponent<MoveBoat>().selected)
                if (!boat.transform.GetComponent<MoveBoat>().smallBoat) {
                    boat.transform.GetComponent<LifeAndPowerDescription>().deployed = !boat.transform.GetComponent<LifeAndPowerDescription>().deployed;
                    action = false;
                } else {
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
        for (int i = 0; i < Player1Boats.transform.childCount; i++)
            Player1Boats.transform.GetChild(i).transform.GetComponent<MoveBoat>().inGame = true;
        for (int i = 0; i < Player2Boats.transform.childCount; i++)
            Player2Boats.transform.GetChild(i).transform.GetComponent<MoveBoat>().inGame = true;
    }
    
    public void ChangeScreen()
    {
        screen++;
        if (screen >= 8)
            screen = 4;
        if (screen > 4)
            setInGame();
        if (gameIsLaunched)
            action = true; 
        this.transform.GetComponent<CannonRange>().setState(false);
        switch(screen) {
            case 0: setActiveThisScreen(interTurn1); break;
            case 1: setActiveThisScreen(placementTurn1); Player1Fog.SetActive(false); Player1Boats.SetActive(true); boats.resetToFull(1); break;
            case 2: setActiveThisScreen(interTurn2); camP1.gameObject.SetActive(false); camP2.gameObject.SetActive(true);Player1Fog.SetActive(true); Player1Boats.SetActive(false); break;
            case 3: setActiveThisScreen(placementTurn2); Player2Fog.SetActive(false); Player2Boats.SetActive(true); boats.resetToFull(2); break;
            case 4: setActiveThisScreen(interTurn1); wind.newDirection(); camP2.gameObject.SetActive(false); camGame.gameObject.SetActive(true); Player2Fog.SetActive(true); Player2Boats.SetActive(false); break;
            case 5: setActiveThisScreen(Turn1); Player1Fog.SetActive(false); Player1Boats.SetActive(true); wind.pushBoats(); Turn1.GetComponent<PlayerTurn>().reset(); ChangeCollisions(true); break;
            case 6: setActiveThisScreen(interTurn2); Player1Fog.SetActive(true); Player1Boats.SetActive(false); break;
            case 7: setActiveThisScreen(Turn2); Player2Fog.SetActive(false); Player2Boats.SetActive(true); wind.pushBoats(); Turn2.GetComponent<PlayerTurn>().reset(); ChangeCollisions(true); break;
        }
    }

    void displayNbRemainingBoats()
    {
        int nbLives = 0;
        int totalLives = 0;
        bool stateSail = false;
        bool selected = false;

        remainingP1.text = "Player 1 has " + Player1Boats.transform.childCount + " boats";
        remainingP2.text = "Player 2 has " + Player2Boats.transform.childCount + " boats";
        GameObject[] boats = GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in boats) {
            if (boat.transform.GetComponent<MoveBoat>().selected) {
                selected = true;
                nbLives = boat.transform.GetComponent<LifeAndPowerDescription>().life;
                totalLives = boat.transform.GetComponent<LifeAndPowerDescription>().totalLife;
                stateSail = boat.transform.GetComponent<LifeAndPowerDescription>().deployed;
            }
        }
        for (int i = 0; i < UIP1.transform.GetChild(1).gameObject.transform.childCount; i++)
            UIP1.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(false);
        for (int i = 0; i < UIP2.transform.GetChild(1).gameObject.transform.childCount; i++)
            UIP2.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(false);
        if (selected) {
            UIP1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(stateSail);
            UIP1.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(!stateSail);
            UIP2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(stateSail);
            UIP2.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(!stateSail);
            for (int i = 0; i < nbLives && i < UIP1.transform.GetChild(1).transform.childCount; i++) {
                UIP1.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(true);
                if (i < nbLives)
                    UIP1.transform.GetChild(1).gameObject.transform.GetChild(i).transform.GetComponent<displayLife>().state = true;
                else
                    UIP1.transform.GetChild(1).gameObject.transform.GetChild(i).transform.GetComponent<displayLife>().state = false;

            }
            for (int i = 0; i < nbLives && i < UIP2.transform.GetChild(1).transform.childCount; i++) {
                UIP2.transform.GetChild(1).gameObject.transform.GetChild(i).gameObject.SetActive(true);
                if (i < nbLives)
                    UIP2.transform.GetChild(1).gameObject.transform.GetChild(i).transform.GetComponent<displayLife>().state = true;
                else
                UIP2.transform.GetChild(1).gameObject.transform.GetChild(i).transform.GetComponent<displayLife>().state = false;
            }
        } else {
            UIP1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            UIP1.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            UIP2.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            UIP2.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void resetGame(int winner)
    {
        resetEndGame = false;
        for (int i = 0; i < Player1Boats.transform.childCount; i++)
            Destroy(Player1Boats.transform.GetChild(i).gameObject);
        for (int i = 0; i < Player2Boats.transform.childCount; i++)
            Destroy(Player2Boats.transform.GetChild(i).gameObject);
        endGame.SetActive(true);
        endGame.transform.GetChild(0).transform.GetChild(1).transform.GetComponent<Text>().text = "Player " + winner.ToString() + " won !";
    }

    void checkEndGame()
    {
        //avengers be like//
        if (!gameIsLaunched)
            return;
        if (Player1Boats.transform.childCount == 0) {
            resetGame(2);
            return;
        }
        if (Player2Boats.transform.childCount == 0) {
            resetGame(1);
            return;
        }
    }

    public void newGame()
    {
        resetEndGame = true;
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

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.GetComponent<CannonRange>().show();
        displayNbRemainingBoats();
        if (resetEndGame)
            checkEndGame();
    }
}
