using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarkingSpaceManager : MonoBehaviour
{
    public GameObject Cabinet_WarningArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Warking_OnTriggerEnter: " + other.gameObject.tag);
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            Cabinet_WarningArea.SetActive(true);
        }
    }
}
