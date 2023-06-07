using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using System.IO;


public class GetPosition : MonoBehaviour {
    //HMD�̈ʒu���W�i�[�p
    private Vector3 HMDPosition;
    //HMD�̉�]���W�i�[�p�i�N�H�[�^�j�I���j
    private Quaternion HMDRotationQ;
    //HMD�̉�]���W�i�[�p�i�I�C���[�p�j
    private Vector3 HMDRotation;

    //���R���g���[���̈ʒu���W�i�[�p
    private Vector3 LeftHandPosition;
    //���R���g���[���̉�]���W�i�[�p�i�N�H�[�^�j�I���j
    private Quaternion LeftHandRotationQ;
    //���R���g���[���̉�]���W�i�[�p
    private Vector3 LeftHandRotation;

    //�E�R���g���[���̈ʒu���W�i�[�p
    private Vector3 RightHandPosition;
    //�E�R���g���[���̉�]���W�i�[�p�i�N�H�[�^�j�I���j
    private Quaternion RightHandRotationQ;
    //�E�R���g���[���̉�]���W�i�[�p
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
    private int PlayMode; //0:���K�A1:�͋[�����A2:�w�K


    void Start() {
        GameManager = GameObject.Find("GameManager");
        SaveData = GameManager.GetComponent<SaveData>();
        GameMode = GameObject.Find("GameMode");
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();

        Timer = GameObject.Find("Time");
    }

    //1�t���[�����ɌĂяo�����Update���]�b�g
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
        //�擾�����f�[�^��\���iHMDP�FHMD�ʒu�CHMDR�FHMD��]�CLFHR�F���R���ʒu�CLFHR�F���R����]�CRGHP�F�E�R���ʒu�CRGHR�F�E�R����]�j
        Debug.Log("HMDP:" + HMDPosition.x + ", " + HMDPosition.y + ", " + HMDPosition.z + "\n" +
                    "HMDR:" + HMDRotation.x + ", " + HMDRotation.y + ", " + HMDRotation.z);
        Debug.Log("LFHP:" + LeftHandPosition.x + ", " + LeftHandPosition.y + ", " + LeftHandPosition.z + "\n" +
                    "LFHR:" + LeftHandRotation.x + ", " + LeftHandRotation.y + ", " + LeftHandRotation.z);
        Debug.Log("RGHP:" + RightHandPosition.x + ", " + RightHandPosition.y + ", " + RightHandPosition.z + "\n" +
                    "RGHR:" + RightHandRotation.x + ", " + RightHandRotation.y + ", " + RightHandRotation.z);
        */

        if (PlayMode == 1) {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            //�擾�f�[�^��CSV�t�@�C���ɋL�q
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
