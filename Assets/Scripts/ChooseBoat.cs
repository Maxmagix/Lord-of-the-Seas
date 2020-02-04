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
    public GameObject prefabButtonBoat;
    public GameObject prefabPlaceBoat;
    public GameObject prefabBuyAnimation;

    public Dictionary<int, String>[] boats = new Dictionary<int, String>[2];
    private int playerChoice = 0;
    public List<GameObject> marketBoats;
    public List<int> priceBoats;
    private int playerTurn;
    public Text priceText;
    public Button buyButton;
    public Text buyText;
    public Text playerTurnText;
    public Text nameActualBoat;
    private int indexboat;
    private int max_money = 0;
    public Text goldText;
    public InputField goldInputField;
    private Dictionary<int, int> player_wallet = new Dictionary<int, int>();
    public GameObject FleetDir;
    private Dictionary<int, GameObject> BoatsP1;
    private Dictionary<int, GameObject> BoatsP2;

    public GameObject PlacementBoatP1;
    public GameObject PlacementBoatP2;

    private int boats_in_column = 6;
    // Start is called before the first frame update

    public void resetToFull(int index)
    {
        int nb_boat = 0;
        int boatsInColumn = 7;

        GameObject[] fleet =  GameObject.FindGameObjectsWithTag("prefabBoatInFleet");
        foreach (GameObject boat in fleet)
            Destroy(boat.gameObject);
        GameObject[] waterBoat =  GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in waterBoat)
            Destroy(boat.gameObject);
        if (index == 1) {
            var newpos = PlacementBoatP1.gameObject.transform.position;
            foreach (GameObject boat in BoatsP1.Values) {
                //copy to placement P1//
                GameObject copyBoat = Instantiate(boat, new Vector3(newpos.x - ((nb_boat / boatsInColumn) * 350), newpos.y - ((nb_boat % (boatsInColumn)) * 100), newpos.z), Quaternion.identity);
                copyBoat.name = nb_boat.ToString();
                copyBoat.transform.SetParent(PlacementBoatP1.transform);
                copyBoat.transform.GetChild(2).transform.GetComponent<Text>().text = boat.gameObject.transform.GetChild(2).transform.GetComponent<Text>().text;
                copyBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{removeBoat(boat.name);});
                BoatsP1[nb_boat] = copyBoat;
                nb_boat++;
            }
        } else if (index == 2) {
            var newpos = PlacementBoatP2.gameObject.transform.position;
            foreach (GameObject boat in BoatsP2.Values) {
                //copy to placement P1//
                GameObject copyBoat = Instantiate(boat, new Vector3(newpos.x - ((nb_boat / boatsInColumn) * 350), newpos.y - ((nb_boat % (boatsInColumn)) * 100), newpos.z), Quaternion.identity);
                copyBoat.name = nb_boat.ToString();
                copyBoat.transform.SetParent(PlacementBoatP2.transform);
                copyBoat.transform.GetChild(2).transform.GetComponent<Text>().text = boat.gameObject.transform.GetChild(2).transform.GetComponent<Text>().text;
                copyBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{removeBoat(boat.name);});
                BoatsP2[nb_boat] = copyBoat;
                nb_boat++;
            }
        }
    }

    private void checkOneBoatAtLeast()
    {
        if (player_wallet[playerTurn] == max_money) {
            confirmButton.gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = new Color(0.6320f, 0.6320f, 0.6320f, 1);
            confirmButton.interactable = false;
        } else {
            confirmButton.gameObject.transform.GetChild(0).transform.GetComponent<Text>().color = new Color(1, 0.8564f, 0, 1);
            confirmButton.interactable = true;
        }
    }

    public void setGold(String goldstr)
    {
        var temp_money = 0;

        Int32.TryParse(goldstr, out temp_money);
        if (temp_money != max_money) {
            if (player_wallet.ContainsKey(0))
                player_wallet[0] = temp_money;
            else
                player_wallet.Add(0, temp_money);
            if (player_wallet.ContainsKey(1))
                player_wallet[1] = temp_money;
            else
                player_wallet.Add(1, temp_money);
        }
        max_money = temp_money;
    }

    private void animateMoney(Color color, String value)
    {
        Text animationText = prefabBuyAnimation.gameObject.transform.GetComponent<Text>();

        animationText.text = value;
        animationText.color = color;
        GameObject animation = Instantiate(prefabBuyAnimation, goldText.gameObject.transform.position, Quaternion.identity);
        animation.transform.SetParent(goldText.gameObject.transform);
    }

    public void BuyBoat()
    {
        Color textColor = new Color(0,0,0);

        if (player_wallet[playerTurn] < priceBoats[indexboat])
            return;
        var nb_boat = 0;
        for (; nb_boat < boats[playerTurn].Count; nb_boat++)
            if (!boats[playerTurn].ContainsKey(nb_boat)) {
                Debug.Log(nb_boat);
                break;
            }
        var newpos = FleetDir.gameObject.transform.position;
        boats[playerTurn].Add(nb_boat, marketBoats[indexboat].gameObject.name);
        GameObject newBoat = Instantiate(prefabButtonBoat, new Vector3(newpos.x + ((nb_boat / boats_in_column) * 250), newpos.y - ((nb_boat % boats_in_column) * 75), newpos.z), Quaternion.identity);
        newBoat.transform.SetParent(FleetDir.transform);
        newBoat.name = nb_boat.ToString();
        newBoat.transform.GetChild(2).transform.GetComponent<Text>().text = marketBoats[indexboat].gameObject.name;
        newBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{removeBoat(newBoat.name);});
        ColorUtility.TryParseHtmlString("#FF1400", out textColor);
        animateMoney(textColor, "-" + priceBoats[indexboat]);
        player_wallet[playerTurn] -= priceBoats[indexboat];
    }

    public void removeBoat(String name)
    {
        Color textColor = new Color(0,0,0);
        int value = 0;
        int price = 0;
        int index = 0;
        String boatName = "";

        Int32.TryParse(name, out value);
        boatName = boats[playerTurn][value];
        boats[playerTurn].Remove(value);
        Destroy(FleetDir.transform.Find(name).gameObject);
        ColorUtility.TryParseHtmlString("#00FF08", out textColor);

        foreach (GameObject boat in marketBoats) {
            Debug.Log(boat.gameObject.name + "/" + boatName);
            if (boat.gameObject.name == boatName)
                price = priceBoats[index];
            index++;
        }
        animateMoney(textColor, "+" + price);
        player_wallet[playerTurn] += price;
    }

    public void resetBoats(bool all)
    {
        Color textColor = new Color(0,0,0);

        ColorUtility.TryParseHtmlString("#00FF08", out textColor);
        if (all) {
            boats[0].Clear();
            boats[1].Clear();
            player_wallet[0] = max_money;
            player_wallet[1] = max_money;
            playerTurn = 0;
        } else {
            boats[playerTurn].Clear();
            animateMoney(textColor, "+" + (max_money - player_wallet[playerTurn]));
            player_wallet[playerTurn] = max_money;
        }
        GameObject[] fleet =  GameObject.FindGameObjectsWithTag("prefabBoatInFleet");
        foreach (GameObject boat in fleet)
            Destroy(boat.gameObject);
    }

    private void updateGold()
    {   
        Color textColor = new Color(); 

        if (player_wallet[playerTurn] < priceBoats[indexboat]) {
            ColorUtility.TryParseHtmlString("#FF1400", out textColor);
        } else {
            ColorUtility.TryParseHtmlString("#FFCA00", out textColor);
        }
        priceText.color = textColor;
        buyText.color = textColor;
    }

    private void changePlayer()
    {
        int nb_boat = 0;
        int boatsInColumn = 7;
        GameObject[] fleet =  GameObject.FindGameObjectsWithTag("prefabBoatInFleet");
 
        if (playerTurn == 0) {
            playerTurn++;
            var newpos = PlacementBoatP1.gameObject.transform.position;
            foreach (GameObject boat in fleet) {
                //copy to placement P1//
                GameObject copyBoat = Instantiate(prefabPlaceBoat, new Vector3(newpos.x - ((nb_boat / boats_in_column) * 350), newpos.y - ((nb_boat % boatsInColumn) * 100), newpos.z), Quaternion.identity);
                copyBoat.name = nb_boat.ToString();
                copyBoat.transform.SetParent(PlacementBoatP1.transform);
                copyBoat.transform.GetChild(2).transform.GetComponent<Text>().text = boat.gameObject.transform.GetChild(2).transform.GetComponent<Text>().text;
                copyBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{removeBoat(boat.name);});
                BoatsP1[nb_boat] = copyBoat;
                Destroy(boat.gameObject);
                nb_boat++;
            }
        } else {
            var newpos = PlacementBoatP2.gameObject.transform.position;
            foreach (GameObject boat in fleet) {
                //copy to placement P2//
                GameObject copyBoat = Instantiate(prefabPlaceBoat, new Vector3(newpos.x - ((nb_boat / boats_in_column) * 350), newpos.y - ((nb_boat % boatsInColumn) * 100), newpos.z), Quaternion.identity);
                copyBoat.name = nb_boat.ToString();
                copyBoat.transform.SetParent(PlacementBoatP2.transform);
                copyBoat.transform.GetChild(2).transform.GetComponent<Text>().text = boat.gameObject.transform.GetChild(2).transform.GetComponent<Text>().text;
                copyBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{removeBoat(boat.name);});
                BoatsP2[nb_boat] = copyBoat;
                Destroy(boat.gameObject);
                nb_boat++;
            }
            camera.translate(1);
        }
    }
    void Start()
    {
        BoatsP1 = new Dictionary<int, GameObject>();
        BoatsP2 = new Dictionary<int, GameObject>();
        boats[0] = new Dictionary<int, String>();
        boats[1] = new Dictionary<int, String>();
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

    public void Update()
    {
        checkOneBoatAtLeast();
        setGold(goldInputField.text);
        updateGold();
        playerTurnText.text = "Player " + (playerTurn + 1).ToString() + " turn";
        goldText.text = player_wallet[playerTurn].ToString();
    }


}
