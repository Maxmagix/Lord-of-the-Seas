using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTile : MonoBehaviour
{
    public Vector3 startpos;
    public Vector3 endpos;
    public AudioClip hit;
    public AudioClip miss;

    public float time;
    public float speed;
    public float gravity;
    public float gravitySpeed;
    public bool explodes;
    public Vector3 originaldistance;
    public Vector3 direction;
    public GameObject AudioPlayer;

    Vector3 getDistance()
    {
        Vector3 distance = endpos - this.gameObject.transform.position;
        if (distance.x < 0)
            distance.x *= -1;
                    if (distance.y < 0)
            distance.y *= -1;
                    if (distance.z < 0)
            distance.z *= -1;
        return distance;
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        direction = endpos - startpos;
        originaldistance = getDistance();
        gravity = (originaldistance.x + originaldistance.z) / 2 / 2;
        gravitySpeed = (originaldistance.x + originaldistance.z) / 2 / speed;
        GameObject[] players = GameObject.FindGameObjectsWithTag("AudioPlayer");
        foreach (GameObject player in players) {
            AudioPlayer = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(direction.x / speed, gravity / speed, direction.z / speed);
        gravity -= gravitySpeed;
        time += 1 / speed;
        if (time >= 1) {
            if (explodes)
                AudioPlayer.transform.GetComponent<PlayAudio>().play(hit);
            else
                AudioPlayer.transform.GetComponent<PlayAudio>().play(miss);
            Destroy(this.gameObject);
        }
    }
}
