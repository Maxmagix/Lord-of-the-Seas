using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using Cameras;
using Gameplay;

public class ChooseBoat : MonoBehaviour
{
    [SerializeField] private translateCamera camera;
    public Button confirmButton;
    public GameObject prefabButtonBoat;
    public GameObject prefabPlaceBoat;
    public GameObject prefabBuyAnimation;

    public Dictionary<int, String>[] boats;
    public String[] descriptionBoats;

    public Text descriptionText;

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
    public Slider goldSlider;
    private Dictionary<int, int> player_wallet;
    public GameObject FleetDir;
    private List<string> BoatsP1 = new List<string>();
    private List<string> BoatsP2 = new List<string>();

    public GameObject GridBoatsP1;

    public GameObject GridBoatsP2;

    public GameObject PlacementBoatP1;
    public GameObject PlacementBoatP2;
    public Tile SpawnTileP1;
    public Tile SpawnTileP2;

    private int boats_in_column = 6;
    // Start is called before the first frame update

    public void resetToFull(int index)
    {
        int nb_boat = 0;
        int boatsInColumn = 6;

        GameObject[] fleet =  GameObject.FindGameObjectsWithTag("prefabBoatInFleet");
        foreach (GameObject boat in fleet)
            Destroy(boat.gameObject);
        GameObject[] waterBoat =  GameObject.FindGameObjectsWithTag("MovableBoat");
        foreach (GameObject boat in waterBoat)
            Destroy(boat.gameObject);
        if (index == 1) {
            var newpos = PlacementBoatP1.gameObject.transform.position;
            foreach (string boat in BoatsP1) {
                //copy to placement P1//
                GameObject copyBoat = Instantiate(prefabPlaceBoat, new Vector3(newpos.x - ((nb_boat / boatsInColumn) * 800), newpos.y - 25 - ((nb_boat % boatsInColumn) * 70), newpos.z), Quaternion.identity);
                copyBoat.name = nb_boat.ToString();
                copyBoat.transform.SetParent(PlacementBoatP1.transform);
                copyBoat.transform.localScale = new Vector3(1.1f,1f,1f);
                copyBoat.transform.GetChild(2).transform.GetComponent<Text>().text = boat;
                copyBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{destroyBoat(copyBoat.name);});
                nb_boat++;
            }
        } else if (index == 2) {
            var newpos = PlacementBoatP2.gameObject.transform.position;
            foreach (string boat in BoatsP2) {
                //copy to placement P1//
                GameObject copyBoat = Instantiate(prefabPlaceBoat, new Vector3(newpos.x - ((nb_boat / boatsInColumn) * 800), newpos.y - 25 - ((nb_boat % boatsInColumn) * 70), newpos.z), Quaternion.identity);
                copyBoat.name = nb_boat.ToString();
                copyBoat.transform.SetParent(PlacementBoatP2.transform);
                copyBoat.transform.localScale = new Vector3(1.1f,1f,1f);
                copyBoat.transform.GetChild(2).transform.GetComponent<Text>().text = boat;
                copyBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{destroyBoat(copyBoat.name);});
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

    public void setGold(int temp_money)
    {
        //var temp_money = 0;

        //Int32.TryParse(goldstr, out temp_money);
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


    public void destroyBoat(String name)
    {
        string boatName = "";

        GameObject[] fleet =  GameObject.FindGameObjectsWithTag("prefabBoatInFleet");
        foreach (GameObject boat in fleet)
            if (boat.name == name) {
                boatName = boat.transform.GetChild(2).transform.GetComponent<Text>().text;
                Destroy(boat.gameObject);
            }
        foreach (GameObject boatOfMarket in marketBoats) {
            Debug.Log(boatName + " compared to " + boatOfMarket.gameObject.name);
            if (boatOfMarket.gameObject.name == boatName) {
                if (GridBoatsP1.active) {
                    GameObject newBoat = Instantiate(boatOfMarket.gameObject, GridBoatsP1.transform);
                    newBoat.SetActive(true);
                    newBoat.transform.GetComponent<Gameplay.MoveBoat>().tile = SpawnTileP1;
                    newBoat.transform.position = SpawnTileP1.gameObject.transform.position;
                    Vector2 decal = newBoat.transform.GetComponent<Gameplay.MoveBoat>().decal;
                    newBoat.transform.position += new Vector3(decal.x, 0, decal.y);
                    if (SpawnTileP1.transform.GetComponent<Tile>().boat != null)
                    newBoat.transform.GetComponent<Hitbox>().setHitboxTiles();
                    newBoat.transform.GetComponent<Hitbox>().setOccupiedBoat(true);
                    newBoat.transform.GetComponent<Gameplay.MoveBoat>().DeselectAll();
                    newBoat.transform.GetComponent<Gameplay.MoveBoat>().selected = true;
                    newBoat.transform.GetComponent<Gameplay.MoveBoat>().moveBoatToTile(SpawnTileP1, true);
                }
                if (GridBoatsP2.active) {
                    GameObject newBoat = Instantiate(boatOfMarket.gameObject, GridBoatsP2.transform);
                    newBoat.SetActive(true);
                    newBoat.transform.GetComponent<Gameplay.MoveBoat>().tile = SpawnTileP2;
                    newBoat.transform.position = SpawnTileP2.gameObject.transform.position;
                    Vector2 decal = newBoat.transform.GetComponent<Gameplay.MoveBoat>().decal;
                    newBoat.transform.position += new Vector3(decal.x, 0, decal.y);
                    if (SpawnTileP2.transform.GetComponent<Tile>().boat != null)
                    newBoat.transform.GetComponent<Hitbox>().setHitboxTiles();
                    newBoat.transform.GetComponent<Hitbox>().setOccupiedBoat(true);
                    newBoat.transform.GetComponent<Gameplay.MoveBoat>().DeselectAll();
                    newBoat.transform.GetComponent<Gameplay.MoveBoat>().selected = true;
                    newBoat.transform.GetComponent<Gameplay.MoveBoat>().moveBoatToTile(SpawnTileP2, true);
                }
            }
        }
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
        int boatsInColumn = 6;
        GameObject[] fleet =  GameObject.FindGameObjectsWithTag("prefabBoatInFleet");
 
        if (playerTurn == 0) {
            BoatsP1.Clear();
            playerTurn++;
            var newpos = PlacementBoatP1.gameObject.transform.position;
            foreach (GameObject boat in fleet) {
                //copy to placement P1//
                GameObject copyBoat = Instantiate(prefabPlaceBoat, new Vector3(newpos.x - ((nb_boat / boatsInColumn) * 1000), newpos.y - 50 -  ((nb_boat % boatsInColumn) * 90), newpos.z), Quaternion.identity);
                copyBoat.name = nb_boat.ToString();
                copyBoat.transform.SetParent(PlacementBoatP1.transform);
                copyBoat.transform.GetChild(2).transform.GetComponent<Text>().text = boat.gameObject.transform.GetChild(2).transform.GetComponent<Text>().text;
                copyBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{destroyBoat(boat.name);});
                BoatsP1.Add(boat.gameObject.transform.GetChild(2).transform.GetComponent<Text>().text);
                Destroy(boat.gameObject);
                nb_boat++;
            }
        } else {
            BoatsP2.Clear();
            var newpos = PlacementBoatP2.gameObject.transform.position;
            foreach (GameObject boat in fleet) {
                //copy to placement P2//
                GameObject copyBoat = Instantiate(prefabPlaceBoat, new Vector3(newpos.x - ((nb_boat / boatsInColumn) * 1000), newpos.y - 50 - ((nb_boat % boatsInColumn) * 90), newpos.z), Quaternion.identity);
                copyBoat.name = nb_boat.ToString();
                copyBoat.transform.SetParent(PlacementBoatP2.transform);
                copyBoat.transform.GetChild(2).transform.GetComponent<Text>().text = boat.gameObject.transform.GetChild(2).transform.GetComponent<Text>().text;
                copyBoat.transform.GetChild(3).transform.GetComponent<Button>().onClick.AddListener(delegate{destroyBoat(boat.name);});
                BoatsP2.Add(boat.gameObject.transform.GetChild(2).transform.GetComponent<Text>().text);
                Destroy(boat.gameObject);
                nb_boat++;
            }
            camera.translate(1);
        }
    }

    void resetShop()
    {
        player_wallet = new Dictionary<int, int>();
        boats = new Dictionary<int, String>[2];
        boats[0] = new Dictionary<int, String>();
        boats[1] = new Dictionary<int, String>();
        playerTurn = 0;
        indexboat = 0;
        setGold((int)goldSlider.value);
        changeBoat(0);
        confirmButton.onClick.AddListener(changePlayer);
    }

    void Start()
    {
        resetShop();
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
        descriptionText.text = "Power:\n" + descriptionBoats[indexboat];
    }

    public void Update()
    {
        checkOneBoatAtLeast();
        setGold((int)goldSlider.value);
        updateGold();
        playerTurnText.text = "Player " + (playerTurn + 1).ToString() + " turn";
        goldText.text = player_wallet[playerTurn].ToString();
    }


}
