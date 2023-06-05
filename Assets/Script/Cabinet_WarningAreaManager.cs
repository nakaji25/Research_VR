using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet_WarningAreaManager : MonoBehaviour
{
    private GameObject WarningText;
    private TextMesh warningText;
    private float textCounter = 3.0f;
    private bool textCounter_flag = false;

    // Start is called before the first frame update
    void Start()
    {
        WarningText = GameObject.Find("WarningText");
        warningText = WarningText.gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textCounter_flag)
        {
            textCounter -= Time.deltaTime;
        }
        if (textCounter < 0)
        {
            WarningText.SetActive(false);
            textCounter_flag = false;
            textCounter = 3.0f;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cab_OnTriggerEnter: " + other.gameObject.tag);
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            textCounter = 3.0f;
            warningText.text = "手がキャビネットから出ました。";
            WarningText.SetActive(true);
            textCounter_flag = true;
        }
    }
}
