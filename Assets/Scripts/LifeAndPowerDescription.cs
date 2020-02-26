using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeAndPowerDescription : MonoBehaviour
{
    public int life;
    public bool deployed;
    public float sinkingSpeed;
    public float CPUElapsedTime;
    public bool placingBuoy;
    public GameObject prefabBuoy;
    public GameObject SpecialDir;

    // Start is called before the first frame update
    void Start()
    {
        placingBuoy = false;
        GameObject[] games = GameObject.FindGameObjectsWithTag("Game");
        foreach (GameObject game in games)
            SpecialDir = game;
    }

    public void placeBuoy(GameObject tile)
    {
        placingBuoy = false;
        GameObject[] games = GameObject.FindGameObjectsWithTag("Game");
        foreach (GameObject game in games) {
            if (game.transform.GetComponent<GameLoop>().action == false)
                return;
            game.transform.GetComponent<GameLoop>().action = false;
        }
        GameObject[] buoys = GameObject.FindGameObjectsWithTag("Buoy");
        foreach (GameObject buoy in buoys) {
            if (buoy.transform.GetComponent<DetectEnnemies>().smallBoat == this.gameObject)
                Destroy(buoy.gameObject);
        }
        GameObject newbuoy = Instantiate(prefabBuoy, SpecialDir.transform);
        newbuoy.transform.GetComponent<DetectEnnemies>().smallBoat = this.gameObject;
        newbuoy.transform.GetComponent<DetectEnnemies>().tile = tile.transform.GetComponent<Tile>();
        newbuoy.transform.position = tile.transform.position;
    }

    void makeDisappear()
    {
        if (this.gameObject.transform.position.y > -10) {
            this.gameObject.transform.position -= new Vector3(0, sinkingSpeed * CPUElapsedTime, 0);
        } else {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CPUElapsedTime = Time.deltaTime;
        if (life == 0)
            makeDisappear();
    }
}
