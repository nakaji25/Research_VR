using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.Extras;

public class LaserPointerHandler : MonoBehaviour
{
    //右手用
    public SteamVR_LaserPointer laserPointer;
    //左手用
    // public SteamVR_LaserPointer laserPointer2;

    public Material buttonColor;

    private int PlayMode = -1; //0:練習、1:模擬試験、2:学習

    GameObject GameMode;

    private void Start()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
        GameMode = GameObject.Find("GameMode");
    }

    //レーザーポインターをtargetに焦点をあわせてトリガーをひいたとき
    public void PointerClick(object sender, PointerEventArgs e)
    {
        //3Dオブジェクト「TestCube」の色を赤色に変更する
        if (e.target.name == "Test_btn")
        {
            PlayMode = 1;
            GameMode.GetComponent<GameModeManager>().SetPlayMode(PlayMode);
            GameObject btn = GameObject.Find("Test_btn");
            btn.GetComponent<Renderer>().material.color = Color.red;
            SceneManager.LoadScene("TestScene");
        } 
        else if (e.target.name == "Learning_btn")
        {
            PlayMode = 2;
            GameMode.GetComponent<GameModeManager>().SetPlayMode(PlayMode);
            GameObject btn = GameObject.Find("Learning_btn");
            btn.GetComponent<Renderer>().material.color = Color.red;
            SceneManager.LoadScene("LearningScene");
        }
        else if (e.target.name == "Practice_btn")
        {
            PlayMode = 0;
            GameObject btn = GameObject.Find("Practice_btn");
            btn.GetComponent<Renderer>().material.color = Color.red;
            SceneManager.LoadScene("PracticeScene");
        }

    }

    //レーザーポインターがtargetに触れたとき
    public void PointerInside(object sender, PointerEventArgs e)
    {
        //3Dオブジェクト「TestCube」の色を青色に変更する
        if (e.target.name == "Test_btn")
        {
            GameObject btn = GameObject.Find("Test_btn");
            btn.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (e.target.name == "Learning_btn")
        {
            GameObject btn = GameObject.Find("Learning_btn");
            btn.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (e.target.name == "Practice_btn")
        {
            GameObject btn = GameObject.Find("Practice_btn");
            btn.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    //レーザーポインターがtargetから離れたとき
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        //3Dオブジェクト「TestCube」の色を黄色に変更する
        if (e.target.name == "Test_btn")
        {
            GameObject btn = GameObject.Find("Test_btn");
            btn.GetComponent<Renderer>().material = buttonColor;
        }
        if (e.target.name == "Learning_btn")
        {
            GameObject btn = GameObject.Find("Learning_btn");
            btn.GetComponent<Renderer>().material = buttonColor;
        }
        else if (e.target.name == "Practice_btn")
        {
            GameObject btn = GameObject.Find("Practice_btn");
            btn.GetComponent<Renderer>().material = buttonColor;
        }
    }
}
