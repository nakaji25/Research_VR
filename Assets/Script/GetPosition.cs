using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using System.IO;


public class GetPosition : MonoBehaviour {
    //HMDの位置座標格納用
    private Vector3 HMDPosition;
    //HMDの回転座標格納用（クォータニオン）
    private Quaternion HMDRotationQ;
    //HMDの回転座標格納用（オイラー角）
    private Vector3 HMDRotation;

    //左コントローラの位置座標格納用
    private Vector3 LeftHandPosition;
    //左コントローラの回転座標格納用（クォータニオン）
    private Quaternion LeftHandRotationQ;
    //左コントローラの回転座標格納用
    private Vector3 LeftHandRotation;

    //右コントローラの位置座標格納用
    private Vector3 RightHandPosition;
    //右コントローラの回転座標格納用（クォータニオン）
    private Quaternion RightHandRotationQ;
    //右コントローラの回転座標格納用
    private Vector3 RightHandRotation;

    private SteamVR_Action_Boolean GrabP = SteamVR_Actions.default_GrabPinch;
    private SteamVR_Action_Single Squeeze = SteamVR_Actions.default_Squeeze;
    private float pull;

    private GameObject GameManager;
    private SaveData SaveData;

    private GameObject Timer;
    private float time;
    private string trimmed_time;

    private GameObject GameMode;
    private int PlayMode; //0:練習、1:模擬試験、2:学習


    void Start() {
        GameManager = GameObject.Find("GameManager");
        SaveData = GameManager.GetComponent<SaveData>();
        GameMode = GameObject.Find("GameMode");
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();

        Timer = GameObject.Find("Time");
    }

    //1フレーム毎に呼び出されるUpdateメゾット
    void Update() {

        List<XRNodeState> DevStat = new List<XRNodeState>();
        InputTracking.GetNodeStates(DevStat);

        foreach (XRNodeState s in DevStat) {
            if (s.nodeType == XRNode.Head) {
                s.TryGetPosition(out HMDPosition);
                s.TryGetRotation(out HMDRotationQ);
                HMDRotation = HMDRotationQ.eulerAngles;
            } else if (s.nodeType == XRNode.LeftHand) {
                s.TryGetPosition(out LeftHandPosition);
                s.TryGetRotation(out LeftHandRotationQ);
                LeftHandRotation = LeftHandRotationQ.eulerAngles;
            } else if (s.nodeType == XRNode.RightHand) {
                s.TryGetPosition(out RightHandPosition);
                s.TryGetRotation(out RightHandRotationQ);
                RightHandRotation = RightHandRotationQ.eulerAngles;
            }
        }

        pull = Squeeze.GetLastAxis(SteamVR_Input_Sources.RightHand);

        /*
        //取得したデータを表示（HMDP：HMD位置，HMDR：HMD回転，LFHR：左コン位置，LFHR：左コン回転，RGHP：右コン位置，RGHR：右コン回転）
        Debug.Log("HMDP:" + HMDPosition.x + ", " + HMDPosition.y + ", " + HMDPosition.z + "\n" +
                    "HMDR:" + HMDRotation.x + ", " + HMDRotation.y + ", " + HMDRotation.z);
        Debug.Log("LFHP:" + LeftHandPosition.x + ", " + LeftHandPosition.y + ", " + LeftHandPosition.z + "\n" +
                    "LFHR:" + LeftHandRotation.x + ", " + LeftHandRotation.y + ", " + LeftHandRotation.z);
        Debug.Log("RGHP:" + RightHandPosition.x + ", " + RightHandPosition.y + ", " + RightHandPosition.z + "\n" +
                    "RGHR:" + RightHandRotation.x + ", " + RightHandRotation.y + ", " + RightHandRotation.z);
        */

        if (PlayMode == 1) {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            //取得データをCSVファイルに記述
            SaveData.SaveCsv(trimmed_time,
                             HMDPosition.x.ToString(), HMDPosition.y.ToString(), HMDPosition.z.ToString(),
                             HMDRotationQ.x.ToString(), HMDRotationQ.y.ToString(), HMDRotationQ.z.ToString(), HMDRotationQ.w.ToString(),
                             LeftHandPosition.x.ToString(), LeftHandPosition.y.ToString(), LeftHandPosition.z.ToString(),
                             LeftHandRotationQ.x.ToString(), LeftHandRotationQ.y.ToString(), LeftHandRotationQ.z.ToString(), LeftHandRotationQ.w.ToString(),
                             RightHandPosition.x.ToString(), RightHandPosition.y.ToString(), RightHandPosition.z.ToString(),
                             RightHandRotationQ.x.ToString(), RightHandRotationQ.y.ToString(), RightHandRotationQ.z.ToString(), RightHandRotationQ.w.ToString(),
                             pull.ToString());
        }
    }
}
