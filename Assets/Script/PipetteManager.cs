using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PipetteManager : MonoBehaviour {
    public GameObject pipetteLiquid;
    public GameObject[] pipetteLiquidArray = new GameObject[8];
    private int Area_type = 0; // ピペットと接触したオブジェクトの判定用
    private int pipetteLiquidArea = 0; // ピペットの液体描画をする範囲

    public Material[] pipetteLiquidColor = new Material[2];
    public Material[] usedItemColor = new Material[1];

    float count = 0.25f;
    private bool counter_flag = false;
    private bool newPipetteLiquid_flag = false; //pipetteに新しい培地が入っているとき true
    private bool oldPipetteLiquid_flag = false; //pipetteに古い培地が入っているとき true
    private bool pipetteOutputmode = false; // pipetteが培地を吸い取り切ったら true
    private bool glassAttached_flag = false;

    //ピペットの装着用
    private bool electronicPipetteAttached_flag;
    public GameObject electronicPipette;
    public GameObject pipettePos;
    public GameObject pipetteColliderPos;
    public GameObject pipetteCollider;
    private Quaternion offsetRotation;
    private Quaternion offsetRotation_2;

    //トリガーボタン用変数
    private SteamVR_Action_Single Squeeze = SteamVR_Actions.default_Squeeze;
    private float pull;

    //手順文用変数
    private GameObject AssistText;
    private bool used_flag = false;

    //警告文用変数
    private float textCounter = 3.0f;
    private bool textCounter_flag = false;
    public GameObject WarningText;
    private TextMesh warningText;

    private GameObject GameManager;
    private ErrorData ErrorData;
    private GameObject Timer;
    private string trimmed_time;
    private bool touch_pipette = false;

    private GameObject GameMode;
    private int PlayMode; //0:練習、1:模擬試験、2:学習

    //デバッグ用
    private GameObject LogText;

    // Start is called before the first frame update
    void Start() {
        AssistText = GameObject.Find("AssistText");
        LogText = GameObject.Find("LogText");

        offsetRotation = Quaternion.Inverse(electronicPipette.transform.rotation) * this.transform.rotation;
        offsetRotation_2 = Quaternion.Inverse(pipetteColliderPos.transform.localRotation) * pipetteCollider.transform.localRotation;
        warningText = WarningText.gameObject.GetComponent<TextMesh>();

        GameManager = GameObject.Find("GameManager");
        ErrorData = GameManager.GetComponent<ErrorData>();
        Timer = GameObject.Find("Time");

        GameMode = GameObject.Find("GameMode");
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();
    }

    // Update is called once per frame
    void Update() {
        if (counter_flag == true) {
            count -= Time.deltaTime;
        }

        FixedJoint fixedJoint = electronicPipette.GetComponent<FixedJoint>();
        if (fixedJoint) {
            if (fixedJoint.connectedBody == this.gameObject.GetComponent<Rigidbody>()) {
                this.transform.rotation = electronicPipette.transform.rotation * offsetRotation;
                //pipetteCollider.transform.localRotation = electronicPipette.transform.localRotation * offsetRotation_2;
                Vector3 pos = pipettePos.transform.position;
                Vector3 colpos = pipetteColliderPos.transform.localPosition;
                this.transform.position = pos;
                //pipetteCollider.transform.localPosition = colpos;
            }
        }
        pull = Squeeze.GetLastAxis(SteamVR_Input_Sources.RightHand);

        //警告文切り替え
        if (textCounter_flag) {
            textCounter -= Time.deltaTime;
        }
        if (textCounter < 0) {
            WarningText.SetActive(false);
            textCounter_flag = false;
            textCounter = 3.0f;
        }

        if (touch_pipette) {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            ErrorData.SaveCsv(trimmed_time, " ", "T");
            touch_pipette = false;
        }
    }
    void OnTriggerEnter(Collider collision) {
        //Debug.Log("TriggerEnter: " + collision.gameObject.tag);
        // Debug.Log("attached flask");
        /*if ((collision.gameObject.tag == "LiquidArea") || (collision.gameObject.tag == "WasteArea") || (collision.gameObject.tag == "StockLiquid") || (collision.gameObject.tag == "MainFlask")) {
            count = 2.0f;
        }*/
        if ((collision.gameObject.tag == "P_JointArea")) {
            electronicPipetteAttached_flag = electronicPipette.GetComponent<ElectronicPipetteManager>().GetpipetteAttached_flag();
            FixedJoint fixedJoint = electronicPipette.GetComponent<FixedJoint>();

            if (fixedJoint == null) {
                //ピペットの使用状況確認
                AssistText.GetComponent<AssistTextManager>().PipetteStatus(used_flag);
                //ピペット装着
                electronicPipette.AddComponent<FixedJoint>();
                fixedJoint = electronicPipette.GetComponent<FixedJoint>();
                fixedJoint.connectedBody = this.gameObject.GetComponent<Rigidbody>();
            }
        }
        if (collision.gameObject.tag == "Bottle_WarningArea") {
            touch_pipette = true;
            if (PlayMode == 2) {
                textCounter = 3.0f;
                warningText.text = "フラスコとピペットが接触しました。";
                WarningText.SetActive(true);
                textCounter_flag = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision) {
        //Debug.Log("CollisionEnter: " + collision.gameObject.tag);
        /*if (collision.gameObject.tag == "Beaker_BottleGlass")
        {
            touch_pipette = true;
            if (PlayMode == 2)
            {
                textCounter = 3.0f;
                warningText.text = "フラスコとピペットが接触しました。";
                WarningText.SetActive(true);
                textCounter_flag = true;
            }
        }*/
    }

    void OnTriggerStay(Collider other) {
        //Debug.Log("OnTriggerStay: " + other.gameObject.tag);

        // 培地を吸い取る処理
        if (((other.gameObject.CompareTag("LiquidArea")) || (other.gameObject.CompareTag("StockLiquid"))) && !(pipetteLiquidArray[7].activeSelf)) {
            if (other.gameObject.tag == "LiquidArea") {
                Area_type = 0;
            } else if (other.gameObject.tag == "StockLiquid") {
                Area_type = 1;
            }
            if (pull > 0) {
                counter_flag = true;
                if (pipetteLiquidArray[pipetteLiquidArea].activeSelf == false && (count <= 0)) {
                    InputPipetteLiquid(pipetteLiquidColor[Area_type], pipetteLiquidArray[pipetteLiquidArea]);
                    Debug.Log("pipetteLiquidArray[" + pipetteLiquidArea + "].activeSelf: " + pipetteLiquidArray[pipetteLiquidArea].activeSelf);
                    if (pipetteLiquidArea == 7) {
                        pipetteOutputmode = true; // ピペットの吐き出しを許可する
                        if (Area_type == 0) {
                            GameObject flask = GameObject.Find("Beaker");
                            flask.GetComponent<FlaskManager>().HiddenFlaskLiquid();
                        } else if (Area_type == 1) {
                            GameObject stockFlask = GameObject.Find("StockBeaker");
                            stockFlask.GetComponent<FlaskManager>().HiddenFlaskLiquid();
                        }
                        counter_flag = false;
                    }
                    if (pipetteLiquidArea < 7)
                        pipetteLiquidArea++;
                    count = 0.25f;
                }
            } else {
                counter_flag = false;
            }
        }

        // 培地をBeakerに入れる処理
        if (((other.gameObject.tag == "WasteArea") || ((other.gameObject.tag == "MainFlask") && newPipetteLiquid_flag)) && (pipetteOutputmode)) {
            if (other.gameObject.tag == "WasteArea") {
                Area_type = 0;
            } else if (other.gameObject.tag == "MainFlask") {
                Area_type = 1;
            }
            if (pull > 0) {
                counter_flag = true;
                if (pipetteLiquidArray[pipetteLiquidArea].activeSelf == true && (count <= 0)) {
                    OutputPipetteLiquid(pipetteLiquidArray[pipetteLiquidArea]);
                    if (pipetteLiquidArea == 0) {
                        pipetteOutputmode = false;
                        // pipetteの液体に応じてBeakerの液体を追加
                        if (Area_type == 0) {
                            GameObject wasteFlask = GameObject.Find("WasteBeaker");
                            wasteFlask.GetComponent<FlaskManager>().InputLiuqid(pipetteLiquid.GetComponent<MeshRenderer>().material);
                        } else if (Area_type == 1) {
                            GameObject flask = GameObject.Find("Beaker");
                            flask.GetComponent<FlaskManager>().InputLiuqid(pipetteLiquid.GetComponent<MeshRenderer>().material);
                        }
                        // Beakerに入った培地が新しいか古いかを確認 
                        Material LiquidMaterial = pipetteLiquidArray[pipetteLiquidArea].GetComponent<MeshRenderer>().material;
                        if (LiquidMaterial.name == "Liquid1 (Instance)") {
                            Debug.Log("oldmedia");
                            AssistText.GetComponent<AssistTextManager>().BeakerLiquidStatus(0);
                            oldPipetteLiquid_flag = false;
                        } else if (LiquidMaterial.name == "Liquid2 (Instance)") {
                            Debug.Log("newmedia");
                            AssistText.GetComponent<AssistTextManager>().BeakerLiquidStatus(1);
                            newPipetteLiquid_flag = false;
                        }
                        // ガラス管を使用済みにする
                        GameObject pipette_Collider = transform.GetChild(0).gameObject;
                        used_flag = true;
                        if (PlayMode == 2) {
                            pipette_Collider.GetComponent<MeshRenderer>().material = usedItemColor[0];
                        }
                        AssistText.GetComponent<AssistTextManager>().PipetteStatus(used_flag);
                        counter_flag = false;
                    }
                    if (pipetteLiquidArea > 0)
                        pipetteLiquidArea--;
                    count = 0.25f;
                }
            } else {
                counter_flag = false;
            }
        }
    }
    public void InputPipetteLiquid(Material LiquidColor, GameObject pipetteLiquid) {
        pipetteLiquid.GetComponent<MeshRenderer>().material = LiquidColor;
        pipetteLiquid.SetActive(true);
        if (LiquidColor == pipetteLiquidColor[0]) {
            oldPipetteLiquid_flag = true;
        } else if (LiquidColor == pipetteLiquidColor[1]) {
            newPipetteLiquid_flag = true;
        }
    }
    public void OutputPipetteLiquid(GameObject pipetteLiquid) {
        pipetteLiquid.SetActive(false);
        used_flag = true;
    }
    private void OnAttachedToHand() {
        glassAttached_flag = true;

        //Debug.Log("HandAttached: glass");
        electronicPipetteAttached_flag = electronicPipette.GetComponent<ElectronicPipetteManager>().GetpipetteAttached_flag();
        if (electronicPipetteAttached_flag) {
            FixedJoint fixedJoint = electronicPipette.GetComponent<FixedJoint>();
            if (fixedJoint != null) {
                //Debug.Log("FixedJoint Destroy");
                Destroy(fixedJoint);
            }
        }
    }
    void OnDetachedFromHand() {
        glassAttached_flag = false;
    }

    public bool Usedflag {
        get { return used_flag; }
    }
}
