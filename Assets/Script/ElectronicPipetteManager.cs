using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicPipetteManager : MonoBehaviour
{
    private bool electronicPipetteAttached_flag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    void HandAttachedUpdate()
    {
        Debug.Log("Attaching");
    }
    private void OnAttachedToHand()
    {
        Debug.Log("HandAttached");
        electronicPipetteAttached_flag = true;
    }
    void OnDetachedFromHand()
    {
        Debug.Log("HandDetached");
        electronicPipetteAttached_flag = false;
    }
    public bool GetpipetteAttached_flag()
    {
        return electronicPipetteAttached_flag;
    }
}
