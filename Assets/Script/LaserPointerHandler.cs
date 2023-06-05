using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.Extras;

public class LaserPointerHandler : MonoBehaviour
{
    //�E��p
    public SteamVR_LaserPointer laserPointer;
    //����p
    // public SteamVR_LaserPointer laserPointer2;

    public Material buttonColor;

    private int PlayMode = -1; //0:���K�A1:�͋[�����A2:�w�K

    GameObject GameMode;

    private void Start()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
        GameMode = GameObject.Find("GameMode");
    }

    //���[�U�[�|�C���^�[��target�ɏœ_�����킹�ăg���K�[���Ђ����Ƃ�
    public void PointerClick(object sender, PointerEventArgs e)
    {
        //3D�I�u�W�F�N�g�uTestCube�v�̐F��ԐF�ɕύX����
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

    //���[�U�[�|�C���^�[��target�ɐG�ꂽ�Ƃ�
    public void PointerInside(object sender, PointerEventArgs e)
    {
        //3D�I�u�W�F�N�g�uTestCube�v�̐F��F�ɕύX����
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

    //���[�U�[�|�C���^�[��target���痣�ꂽ�Ƃ�
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        //3D�I�u�W�F�N�g�uTestCube�v�̐F�����F�ɕύX����
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
