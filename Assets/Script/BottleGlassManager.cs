using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGlassManager : MonoBehaviour
{

    private bool FlaskAttached_flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnAttachedToHand()
    {
        //Debug.Log("HandAttached: Flask");
        FlaskAttached_flag = true;
    }
    void OnDetachedFromHand()
    {
        //Debug.Log("HandDetached: Flask");
        FlaskAttached_flag = false;
    }
    public bool GetFlaskAttached_flag()
    {
        return FlaskAttached_flag;
    }
}
