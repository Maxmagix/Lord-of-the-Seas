using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class DisappearMoney : MonoBehaviour
{
    public float speedMove;
    public float speedFade;
    private Text price;
    private float opacity;
    // Start is called before the first frame update
    void Start()
    {
        opacity = 1;
        price = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (opacity <= 0)
            Destroy(gameObject);
        var pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(pos.x, pos.y + speedMove, pos.z);
        Color colorprice = price.color;
        colorprice.a = opacity;
        price.color = colorprice;
        opacity -= speedFade / 255;
    }
}
