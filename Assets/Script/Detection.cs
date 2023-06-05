using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public GameObject area_1;
    public GameObject area_2;
    public GameObject area_3;
    public GameObject area_4;
    public GameObject flaskCollider;
    public GameObject flask;
    public GameObject CellArea;

    private Vector3 area1pos;
    private Vector3 area2pos;
    private Vector3 area3pos;
    private Vector3 area4pos;
    private Vector3 flaskpos;

    float min;
    private bool hideenFlaskLiquid_flag;

    private Transform flaskTransform;
    private Vector3 flaskRotation;

    //フラスコの傾きのFB用
    private GameObject AssistText;
    public GameObject WarningText;
    private TextMesh warningText;
    private bool flaskAttached_Flag = false;
    private bool LiquidTask = false;

    private GameObject GameMode;
    private int PlayMode; //0:練習、1:模擬試験、2:学習


    // Start is called before the first frame update
    void Start()
    {
        AssistText = GameObject.Find("AssistText");
        warningText = WarningText.gameObject.GetComponent<TextMesh>();

        GameMode = GameObject.Find("GameMode");
        PlayMode = GameMode.GetComponent<GameModeManager>().GetPlayMode();
    }

    // Update is called once per frame
    void Update()
    {
        flaskpos = flaskCollider.transform.eulerAngles;

        hideenFlaskLiquid_flag = flask.GetComponent<FlaskManager>().GetHiddenFlaskLiquid_flag();
        if (hideenFlaskLiquid_flag == false)
        {
            area1pos = area_1.transform.position;
            area2pos = area_2.transform.position;
            area3pos = area_3.transform.position;
            area4pos = area_4.transform.position;

            min = Mathf.Min(area1pos.y, area2pos.y, area3pos.y, area4pos.y);

            if (flaskCollider.tag == "Beaker_BottleGlass")
            {
                if ((flaskpos.z > 265 && flaskpos.z < 275))
                {
                    CellArea.SetActive(true);
                    area_1.SetActive(false);
                    area_2.SetActive(false);
                    area_3.SetActive(false);
                    area_4.SetActive(false);
                }
                else if ((flaskpos.x < 20 || flaskpos.x > 340) && (flaskpos.z < 20 || flaskpos.z > 340))
                {
                    CellArea.SetActive(false);
                    area_1.SetActive(true);
                    area_2.SetActive(true);
                    area_3.SetActive(true);
                    area_4.SetActive(true);
                }
                else
                {
                    if (min == area1pos.y)
                    {
                        CellArea.SetActive(false);
                        area_1.SetActive(true);
                        area_2.SetActive(false);
                        area_3.SetActive(false);
                        area_4.SetActive(false);
                    }
                    if (min == area2pos.y)
                    {
                        CellArea.SetActive(false);
                        area_1.SetActive(false);
                        area_2.SetActive(true);
                        area_3.SetActive(false);
                        area_4.SetActive(false);
                    }
                    if (min == area3pos.y)
                    {
                        CellArea.SetActive(false);
                        area_1.SetActive(false);
                        area_2.SetActive(false);
                        area_3.SetActive(true);
                        area_4.SetActive(false);
                    }
                    if (min == area4pos.y)
                    {
                        CellArea.SetActive(false);
                        area_1.SetActive(false);
                        area_2.SetActive(false);
                        area_3.SetActive(false);
                        area_4.SetActive(true);
                    }

                }
            }
            else
            {
                if ((flaskpos.x < 20 || flaskpos.x > 340) && (flaskpos.z < 20 || flaskpos.z > 340))
                {
                    area_1.SetActive(true);
                    area_2.SetActive(true);
                    area_3.SetActive(true);
                    area_4.SetActive(true);
                }
                else
                {
                    if (min == area1pos.y)
                    {
                        area_1.SetActive(true);
                        area_2.SetActive(false);
                        area_3.SetActive(false);
                        area_4.SetActive(false);
                    }
                    if (min == area2pos.y)
                    {
                        area_1.SetActive(false);
                        area_2.SetActive(true);
                        area_3.SetActive(false);
                        area_4.SetActive(false);
                    }
                    if (min == area3pos.y)
                    {
                        area_1.SetActive(false);
                        area_2.SetActive(false);
                        area_3.SetActive(true);
                        area_4.SetActive(false);
                    }
                    if (min == area4pos.y)
                    {
                        area_1.SetActive(false);
                        area_2.SetActive(false);
                        area_3.SetActive(false);
                        area_4.SetActive(true);
                    }
                }
            }
        }
        flaskTransform = flaskCollider.transform;
        flaskRotation = flaskTransform.eulerAngles;
        //Debug.Log(flask.tag + "：" + flaskRotation);

        if (PlayMode == 2)
        {
            if (flaskCollider.tag == "Beaker_BottleGlass" || flaskCollider.tag == "Stock_BottleGlass")
            {
                flaskAttached_Flag = flaskCollider.GetComponent<BottleGlassManager>().GetFlaskAttached_flag();
                LiquidTask = AssistText.GetComponent<AssistTextManager>().GetLiquidTask_flag();
                if (LiquidTask)
                {
                    if (flaskAttached_Flag)
                    {
                        Debug.Log("flaskAttached_Flag");
                        if ((flaskpos.z > 310) || (flaskpos.z < 10))
                        {
                            warningText.text = "フラスコをもっと傾けてください。";
                            WarningText.SetActive(true);
                        }
                        else if ((flaskpos.z < 280) && (flaskpos.z >= 180))
                        {
                            warningText.text = "フラスコを傾けすぎです。";
                            WarningText.SetActive(true);
                        }
                        else if ((flaskpos.z > 10) && (flaskpos.z < 180))
                        {
                            warningText.text = "傾ける方向が逆です。";
                            WarningText.SetActive(true);
                        }
                    }
                    else
                    {
                        WarningText.SetActive(false);
                    }
                }
            }
        }
    }
    private void OnAttachedToHand()
    {
        
    }
    private void HandAttachedUpdate()
    {

    }
    void OnDetachedFromHand()
    {
        
    }
}
