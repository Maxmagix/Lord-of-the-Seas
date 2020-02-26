using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public GameObject soundPlayerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void play(AudioClip clip)
    {
        GameObject sound = Instantiate(soundPlayerPrefab, this.transform);
        sound.transform.GetComponent<AudioSource>().clip = clip;
        sound.transform.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
