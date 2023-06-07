using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskManager : MonoBehaviour {
    public GameObject area_1;
    public GameObject area_2;
    public GameObject area_3;
    public GameObject area_4;
    private bool hiddenFlaskLiquid_flag = false;
    public Material[] pipetteLiquidColor = new Material[2];
    public GameObject CellArea;


    // Start is called before the first frame update
    void Start() {
        if (gameObject.tag == "WasteFlask") {
            hiddenFlaskLiquid_flag = true;
        }
    }

    // Update is called once per frame
    void Update() {

    }
    public void HiddenFlaskLiquid() {
        Debug.Log("hidden");
        area_1.SetActive(false);
        area_2.SetActive(false);
        area_3.SetActive(false);
        area_4.SetActive(false);
        if (CellArea != null) {
            CellArea.SetActive(false);
        }
        hiddenFlaskLiquid_flag = true;
    }
    public void InputLiuqid(Material pipetteLiuqid) {
        //Debug.Log("Input Liuqid");
        area_1.GetComponent<MeshRenderer>().material = pipetteLiuqid;
        area_2.GetComponent<MeshRenderer>().material = pipetteLiuqid;
        area_3.GetComponent<MeshRenderer>().material = pipetteLiuqid;
        area_4.GetComponent<MeshRenderer>().material = pipetteLiuqid;
        if (CellArea != null) {
            CellArea.GetComponent<MeshRenderer>().material = pipetteLiuqid;
        }
        hiddenFlaskLiquid_flag = false;
    }
    public bool GetHiddenFlaskLiquid_flag() {
        return hiddenFlaskLiquid_flag;
    }
}
