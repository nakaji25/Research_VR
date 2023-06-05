using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaminationManager : MonoBehaviour
{
    public GameObject flask;
    public Material[] flaskColor = new Material[2];

    private float textCounter = 3.0f;
    private bool textCounter_flag = false;
    private bool con_flag = false;

    public GameObject WarningText;
    private TextMesh warningText;

    private GameObject GameManager;
    private ErrorData ErrorData;
    private GameObject Timer;
    private string trimmed_time;
    private float time = 0f;

    private GameObject GameMode;
    private int PlayMode = 2; //0:練習、1:模擬試験、2:学習

    // Start is called before the first frame update
    void Start()
    {
        warningText = WarningText.gameObject.GetComponent<TextMesh>();

        GameManager = GameObject.Find("GameManager");
        ErrorData = GameManager.GetComponent<ErrorData>();
        Timer = GameObject.Find("Time");

        GameMode = GameObject.Find("GameMode");
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (textCounter_flag)
        {
            textCounter -= Time.deltaTime;
        }
        if (textCounter < 0)
        {
            WarningText.SetActive(false);
            flask.GetComponent<MeshRenderer>().material = flaskColor[0];
            textCounter_flag = false;
            textCounter = 3.0f;
        }

        if (con_flag)
        {
            trimmed_time = Timer.GetComponent<TimeManager>().GetTime();
            ErrorData.SaveCsv(trimmed_time, "T", " ");
            con_flag = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("con_CollisionEnter: " + collision.gameObject.tag);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("con_OnTriggerEnter: " + other.gameObject.tag);
        if (other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            con_flag = true;
            if (PlayMode == 2)
            {
                flask.GetComponent<MeshRenderer>().material = flaskColor[1];
                textCounter = 3.0f;
                warningText.text = "フラスコが汚染されました。";
                WarningText.SetActive(true);
                textCounter_flag = true;
            }             
        }
    }
}
