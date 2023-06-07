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
            string[] s1 = { "Time", "HMDP.x", "HMDP.y", "HMDP.z", "HMDR_Q.x", "HMDR_Q.y", "HMDR_Q.z", "HMDR_Q.w", "LeftHandP.x", "LeftHandP.y", "LeftHandP.z", "LeftHandR_Q.x", "LeftHandR_Q.y", "LeftHandR_Q.z", "LeftHandR_Q.w", "RightHandP.x", "RightHandP.y", "RightHandP.z", "RightHandR_Q.x", "RightHandR_Q.y", "RightHandR_Q.z", "RightHandR_Q.w", "TriggerPull" };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            sw.Close();
        }
    }

    public void SaveCsv(string txt1, string txt2, string txt3, string txt4, string txt5, string txt6, string txt7, string txt8, string txt9, string txt10, string txt11, string txt12, string txt13, string txt14, string txt15, string txt16, string txt17, string txt18, string txt19, string txt20, string txt21, string txt22, string txt23) {
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();
        //Debug.Log(PlayMode);
        if (PlayMode == 1) {
            sw = new StreamWriter(@"SaveData.csv", true, Encoding.GetEncoding("Shift_JIS"));
            string[] s1 = { txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11, txt12, txt13, txt14, txt15, txt16, txt17, txt18, txt19, txt20, txt21, txt22, txt23 };
            string s2 = string.Join(",", s1);
            sw.WriteLine(s2);
            sw.Close();
        }
    }

    void Update() {

    }
}
