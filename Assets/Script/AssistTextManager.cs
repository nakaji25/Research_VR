using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssistTextManager : MonoBehaviour {

    public GameObject Beaker;
    public GameObject StockBeaker;
    public GameObject WasteBeaker;
    public GameObject electronicPipette;
    public GameObject cellArea;
    private GameObject Beaker_Glass;
    private GameObject StockBeaker_Glass;
    private GameObject WasteBeaker_Glass;

    private TextMesh assistText;
    private bool oldmedia = true;
    private bool newmedia = false;
    private bool used_flag = false;
    private bool finish_flag = false;
    private bool LiquidTask_flag = false;
    private bool flaskMedia = true;
    private int BeakerLiquidColor = 0; //0:青,1:赤
    private bool AttachedFlask = false;

    private GameObject GameMode;
    private int PlayMode; //0:練習、1:模擬試験、2:学習

    private GameObject Timer;
    private float time;
    private string trimmed_time;

    private GameObject GameManager;
    private SaveData SaveData;

    int afterTask = 1;
    bool finishedTask = false;

    // Start is called before the first frame update
    void Start() {
        Beaker_Glass = Beaker.transform.Find("Beaker_Bottle_Glass").gameObject;
        StockBeaker_Glass = StockBeaker.transform.GetChild(0).gameObject;
        WasteBeaker_Glass = WasteBeaker.transform.GetChild(0).gameObject;

        GameObject AssistText = GameObject.Find("AssistText");
        assistText = AssistText.gameObject.GetComponent<TextMesh>();
        assistText.text = "培養フラスコと廃棄用のフラスコを開け\n電子ピペットにガラス管を装着して下さい。";

        GameManager = GameObject.Find("GameManager");
        SaveData = GameManager.GetComponent<SaveData>();
        Timer = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update() {
        AssistTextChange();
        if (finishedTask) {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            //取得データをCSVファイルに記述
            SaveData.SaveCsv(trimmed_time, "Task" + afterTask, "Finish", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-");
            afterTask += 1;
            finishedTask = false;
        }
    }

    public void AssistTextChange() {
        FixedJoint pipetteJoint = electronicPipette.GetComponent<FixedJoint>();
        FixedJoint BeakerJoint = Beaker_Glass.GetComponent<FixedJoint>();
        FixedJoint StockBeakerJoint = StockBeaker_Glass.GetComponent<FixedJoint>();
        FixedJoint WasteBeakerJoint = WasteBeaker_Glass.GetComponent<FixedJoint>();
        flaskMedia = !(Beaker.GetComponent<FlaskManager>().GetHiddenFlaskLiquid_flag());
        AttachedFlask = Beaker_Glass.GetComponent<BottleGlassManager>().GetFlaskAttached_flag();

        if ((pipetteJoint != null) && used_flag) {
            if (afterTask == 2 || afterTask == 6) {
                finishedTask = true;
            }
            assistText.text = "使用済みのガラス管を取り外し捨てて下さい。";
            LiquidTask_flag = false;
        } else if (flaskMedia) {
            //Debug.Log("flaskMedia: true");
            if ((pipetteJoint != null) && (BeakerJoint == null) && (WasteBeakerJoint == null) && (oldmedia)) {
                if (afterTask == 1) {
                    finishedTask = true;
                }
                assistText.text = "古い培地（青）を吸い取り捨てて下さい。";
                LiquidTask_flag = true;
            }
            if ((pipetteJoint == null) && (BeakerJoint == null) && (StockBeakerJoint == null) && (newmedia)) {
                if (afterTask == 7) {
                    finishedTask = true;
                }
                assistText.text = "全てのフラスコを閉めて培養フラスコを\n横にして置いて下さい。";
            }
            if ((cellArea.activeSelf) && !(AttachedFlask) && (pipetteJoint == null) && (BeakerJoint != null) && (StockBeakerJoint != null) && (WasteBeakerJoint != null) && (newmedia)) {
                assistText.text = "終了";
                finish_flag = true;
                Invoke("BackToMenu", 2f);
            }
        } else if (!(flaskMedia)) {
            if ((pipetteJoint == null)) {
                if ((StockBeakerJoint != null) && (WasteBeakerJoint == null)) {
                    if (afterTask == 3) {
                        finishedTask = true;
                    }
                    assistText.text = "廃棄用のフラスコを閉じ、新しい培地（赤）の\n入ったフラスコを開けて下さい。";
                }
                if ((StockBeakerJoint == null) && (WasteBeakerJoint != null)) {
                    if (afterTask == 4) {
                        finishedTask = true;
                    }
                    assistText.text = "新しいガラス管を装着して下さい。";
                }
            } else if ((pipetteJoint != null) && !(used_flag)) {
                if ((BeakerJoint == null) && (StockBeakerJoint == null)) {
                    if (afterTask == 5) {
                        finishedTask = true;
                    }
                    assistText.text = "新しい培地（赤）を吸い取り培養フラスコに\n入れて下さい。";
                    LiquidTask_flag = true;
                }
            }
        }
    }

    public void BeakerLiquidStatus(int LiquidColor) {
        BeakerLiquidColor = LiquidColor;
        //Beakerの中の培地が古いか新しいか確認
        if (BeakerLiquidColor == 0) {
            //Debug.Log("flaskMedia: old");
            oldmedia = true;
            newmedia = false;
        } else if (BeakerLiquidColor == 1) {
            //Debug.Log("flaskMedia: new");
            oldmedia = false;
            newmedia = true;
        }
    }
    public void PipetteStatus(bool UsedPipette) {
        used_flag = UsedPipette;
    }
    public bool GetFinish_flag() {
        return finish_flag;
    }
    public bool GetLiquidTask_flag() {
        return LiquidTask_flag;
    }
    void BackToMenu() {
        SceneManager.LoadScene("MenuScene");
    }
}
