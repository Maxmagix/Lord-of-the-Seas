using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinking : MonoBehaviour
{
    private float startpoint;
    private bool sinking;


    public float maxdepth;
    public float speedMov;


    
    // Start is called before the first frame update
    void Start()
    {
        sinking = true;
        startpoint = this.gameObject.transform.position.y;    
        speedMov = Random.Range(0.005f, 0.01f);

    }


    // Update is called once per frame
    void Update()
    {
        //var timeSpent = Time.deltaTime;

        this.gameObject.transform.position += new Vector3(0, speedMov * (sinking ? -1 : 1), 0);
        if (this.gameObject.transform.position.y < startpoint - maxdepth) {
            speedMov = Random.Range(0.005f, 0.01f);
            sinking = false;
        }
        if (this.gameObject.transform.position.y > startpoint) {
            speedMov = Random.Range(0.005f, 0.01f);
            sinking = true;
        }
    }
}
