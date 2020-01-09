using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsMenu : MonoBehaviour
{
    public Slider volumeMusic;
    public AudioSource MusicPlayer;
    // Start is called before the first frame update
    void Start()
    {
        volumeMusic.value = volumeMusic.maxValue / 2;
    }

    // Update is called once per frame
    void Update()
    {
        MusicPlayer.volume = volumeMusic.value / volumeMusic.maxValue;
    }
}
