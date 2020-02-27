using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayLife : MonoBehaviour
{
    public Texture lifeBottle;
    public Texture deathBottle;
    public bool state;

    // Start is called before the first frame update
    void Start()
    {
        state = true;   
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
            this.transform.GetComponent<RawImage>().texture = lifeBottle;
        else
            this.transform.GetComponent<RawImage>().texture = deathBottle;
    }
}