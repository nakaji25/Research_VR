using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningAreaManager : MonoBehaviour {
    public GameObject warningArea;
    public GameObject flask;
    public Material[] usedItemColor = new Material[1];
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("warningArea: " + other.gameObject.tag);
        if ((other.gameObject.tag == "LeftHand") || (other.gameObject.tag == "RightHand")) {
            flask.GetComponent<MeshRenderer>().material = usedItemColor[0];
        }
    }
}
