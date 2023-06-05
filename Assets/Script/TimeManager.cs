using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private float time;
    private int minute;
    private float seconds;
    //�@�O��Update�̎��̕b��
    private float oldSeconds;
    //�@�^�C�}�[�\���p�e�L�X�g
    private TextMesh timerText;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        minute = 0;
        seconds = 0f;
        oldSeconds = 0f;
        //GameObject timer = GameObject.Find("Time");
        timerText = this.gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        seconds += Time.deltaTime;
        if (seconds >= 60f)
        {
            minute++;
            seconds = seconds - 60;
        }
        //�@�l���ς�����������e�L�X�gUI���X�V
        
        timerText.text = minute.ToString("00") + ":" + seconds.ToString("00.00");
      
    }

    public string GetTime()
    {
        string trimmed_time = time.ToString();
        return trimmed_time;
    }
}
