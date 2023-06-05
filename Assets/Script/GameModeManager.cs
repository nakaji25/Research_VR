using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour {
    private int PlayMode = 2;

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update() {

    }
    public void SetPlayMode(int SelectMode) {
        PlayMode = SelectMode;
    }

    public int GetPlayMode() {
        return PlayMode;
    }
}
