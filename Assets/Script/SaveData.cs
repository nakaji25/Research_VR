using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SaveData : MonoBehaviour {
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
            sw = new StreamWriter(@"SaveData.csv", false, Encoding.GetEncoding("Shift_JIS"));
            string[] s1 = { "Time", "HMDP.x", "HMDP.y", "HMDP.z", "HMDR.x", "HMDR.y", "HMDR.z", "LeftHandP.x", "LeftHandP.y", "LeftHandP.z", "LeftHandR.x", "LeftHandR.y", "LeftHandR.z", "RightHandP.x", "RightHandP.y", "RightHandP.z", "RightHandR.x", "RightHandR.y", "RightHandR.z", "TriggerPull" };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            sw.Close();
        }
    }

    public void SaveCsv(string txt1, string txt2, string txt3, string txt4, string txt5, string txt6, string txt7, string txt8, string txt9, string txt10, string txt11, string txt12, string txt13, string txt14, string txt15, string txt16, string txt17, string txt18, string txt19, string txt20) {
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();
        //Debug.Log(PlayMode);
        if (PlayMode == 1) {
            sw = new StreamWriter(@"SaveData.csv", true, Encoding.GetEncoding("Shift_JIS"));
            string[] s1 = { txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11, txt12, txt13, txt14, txt15, txt16, txt17, txt18, txt19, txt20 };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            sw.Close();
        }
    }

    void Update() {

    }
}
