using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicPlayer : MonoBehaviour
{
    public AudioClip[] Menu;
    public AudioClip[] Metal;
    public AudioClip[] Movie;
    public AudioClip[] Normal;

    public Toggle MetalTrigger;
    public Toggle MovieTrigger;
    public Toggle NormalTrigger;

    public string mode;

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
            changeMusic(mode);
    }

    public void setMode(string newmode)
    {
        if (newmode == mode)
            return;
        mode = newmode;
        changeMusic(mode);
    }

    void chooseRandomSong(List<AudioClip[]> clips)
    {
        int style = Random.Range(0, clips.Count);
        GetComponent<AudioSource>().clip = clips[style][Random.Range(0, clips[style].Length)];
    }

    public void changeMusic(string mode)
    {
        List<AudioClip[]> selection = new List<AudioClip[]>();
        if (mode == "Menu") {
            selection.Add(Menu);
            chooseRandomSong(selection);
        }
        if (mode == "Game") {
            if (MetalTrigger.isOn)
                selection.Add(Metal);
            if (MovieTrigger.isOn)
                selection.Add(Movie);
            if ((!MetalTrigger.isOn && !MovieTrigger.isOn) || NormalTrigger.isOn)
                selection.Add(Normal);
            chooseRandomSong(selection);
        }
        GetComponent<AudioSource>().Play();
    }
}
