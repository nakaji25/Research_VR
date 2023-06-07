using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorWrite : MonoBehaviour {
    private GameObject GameManager;
    private ErrorData ErrorData;
    private GameObject Timer;
    private string trimmed_time;

    private bool con_flag = false;
    private bool touch_pipette = false;

    // Start is called before the first frame update
    void Start() {
        GameManager = GameObject.Find("GameManager");
        ErrorData = GameManager.GetComponent<ErrorData>();
        Timer = GameObject.Find("Time");

    }

    // Update is called once per frame
    void Update() {
        if (con_flag && touch_pipette) {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            ErrorData.SaveCsv(trimmed_time, "T", "T");
            con_flag = false;
            touch_pipette = false;
        } else if (con_flag) {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            ErrorData.SaveCsv(trimmed_time, "T", " ");
            con_flag = false;
        } else if (touch_pipette) {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            ErrorData.SaveCsv(trimmed_time, " ", "T");
            touch_pipette = false;
        } else {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            ErrorData.SaveCsv(trimmed_time, " ", " ");
        }
    }

    public void SetConFlag(bool flag) {
        con_flag = flag;
    }
    public void SetTouchPipette(bool flag) {
        touch_pipette = flag;
    }
}
