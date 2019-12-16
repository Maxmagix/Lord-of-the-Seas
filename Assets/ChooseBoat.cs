using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using Cameras;
public class ChooseBoat : MonoBehaviour
{
    
    [SerializeField] private translateCamera camera;
    public Button confirmButton;
    public List<GameObject>[] boats;
    private int playerChoice = 0;
    public List<GameObject> marketBoats;
    public List<int> priceBoats;
    private int playerTurn;
    public Text priceText;
    public Text playerTurnText;
    public Text nameActualBoat;
    private int indexboat;
    private int max_money;
    public Text goldText;
    public InputField goldInputField;
    private int[] player_wallet = new int[2];
    
    // Start is called before the first frame update

    public void setGold(String goldstr)
    {
        Int32.TryParse(goldstr, out max_money);
    }

    private void changePlayer()
    {
        if (playerTurn == 0)
            playerTurn++;
        else {
            camera.translate(1);
        }
    }    
    void Start()
    {
        player_wallet[0] = 0;
        player_wallet[1] = 0;
        playerTurn = 0;
        indexboat = 0;
        setGold(goldInputField.text);
        changeBoat(0);
        confirmButton.onClick.AddListener(changePlayer);
    }

    public void changeBoat(int dir)
    {
        GameObject  ChildGameObject = marketBoats[indexboat].gameObject;
        ChildGameObject.SetActive(false);
        if (indexboat + dir < 0)
            indexboat = marketBoats.ToArray().Length - 1;
        else if (indexboat + dir >= marketBoats.ToArray().Length)
            indexboat = 0;
        else
            indexboat += dir;
        ChildGameObject = marketBoats[indexboat].gameObject;
        ChildGameObject.SetActive(true);
        nameActualBoat.text = ChildGameObject.name;
        priceText.text = priceBoats[indexboat].ToString();
    }

    public void buyBoat()
    {
    }

    public void Update()
    {
        setGold(goldInputField.text);
        playerTurnText.text = "Player " + (playerTurn + 1).ToString() + " turn"; 
        goldText.text = (max_money).ToString();
    } 

    
}
