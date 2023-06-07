using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ErrorData : MonoBehaviour {
    // Start is called before the first frame update
    private StreamWriter sw;
    private GameObject AssistText;

    private bool finish_flag;

    private GameObject GameMode;
    private int PlayMode; //0:ó˚èKÅA1:ñÕã[ééå±ÅA2:äwèK

    void Start() {
        GameMode = GameObject.Find("GameMode");
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();
        AssistText = GameObject.Find("AssistText");
        if (PlayMode == 1) {
            sw = new StreamWriter(@"ErrorData.csv", false, Encoding.GetEncoding("Shift_JIS"));
            string[] s1 = { "Time", "Contamination", "Touch_Pipette" };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            sw.Close();
        }
    }

    public void SaveCsv(string txt1, string txt2, string txt3) {
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();
        if (PlayMode == 1) {
            sw = new StreamWriter(@"ErrorData.csv", true, Encoding.GetEncoding("Shift_JIS"));
            string[] s1 = { txt1, txt2, txt3 };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            //Debug.Log(s2);
            sw.Close();
        }
    }
    void Update() {

    }
}
