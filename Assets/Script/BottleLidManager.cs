using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleLidManager : MonoBehaviour {
    public GameObject Flask;
    private bool FlaskAttached_flag;
    public GameObject bottleLidPos;
    private Quaternion offsetRotation;
    public GameObject con_Trigger;
    private float counter = 2.0f;
    private bool counter_flag = false;
    private bool joint_flag = false;
    private bool LidAttached_flag = false;

    private GameObject AssistText;

    // Start is called before the first frame update
    void Start() {
        AssistText = GameObject.Find("AssistText");
        offsetRotation = Quaternion.Inverse(Flask.transform.rotation) * this.transform.rotation;
    }

    // Update is called once per frame
    void Update() {
        if (counter_flag == true) {
            counter -= Time.deltaTime;
        }
        FixedJoint fixedJoint = Flask.GetComponent<FixedJoint>();
        if (counter < 0 && (fixedJoint == null)) {
            con_Trigger.SetActive(true);
            counter_flag = false;
            counter = 2.0f;
        }
        if (fixedJoint != null) {
            if (fixedJoint.connectedBody == this.gameObject.GetComponent<Rigidbody>()) {
                //this.transform.rotation = Flask.transform.rotation * offsetRotation;
                Vector3 pos = bottleLidPos.transform.position;
                this.transform.position = pos;
            }
        }

    }
    private void OnCollisionEnter(Collision collision) {
        //Debug.Log("Lid_CollisionEnter: " + collision.gameObject.tag);
        // キャップの取り付け
        if (joint_flag) {
            con_Trigger.SetActive(false);
            //if ((collision.gameObject.tag == "Beaker_BottleGlass") || (collision.gameObject.tag == "Stock_BottleGlass") || (collision.gameObject.tag == "Waste_BottleGlass"))
            if (collision.gameObject.tag == Flask.tag)
                if (collision.gameObject.tag == Flask.tag) {
                    FlaskAttached_flag = Flask.GetComponent<BottleGlassManager>().GetFlaskAttached_flag();
                    FixedJoint fixedJoint = Flask.GetComponent<FixedJoint>();
                    Debug.Log("CollisionEnter: BottleGlass");
                    if (fixedJoint == null) {
                        Flask.AddComponent<FixedJoint>();
                        fixedJoint = Flask.GetComponent<FixedJoint>();
                        fixedJoint.connectedBody = this.gameObject.GetComponent<Rigidbody>();
                        Debug.Log("FixedJoint");
                    }
                }
        }

    }
    private void OnAttachedToHand() {
        //Debug.Log("HandAttached:Lid");
        con_Trigger.SetActive(false);
        FlaskAttached_flag = Flask.GetComponent<BottleGlassManager>().GetFlaskAttached_flag();
        // キャップの取り外し
        if (FlaskAttached_flag) {
            Debug.Log("FixedJoint Destroy");
            FixedJoint fixedJoint = Flask.GetComponent<FixedJoint>();
            if (fixedJoint != null) {
                Destroy(fixedJoint);


                fixedJoint = Flask.GetComponent<FixedJoint>();
                if (fixedJoint == null) {
                    counter = 2.0f;
                    counter_flag = true;
                }
            }
        }
    }
    private void OnDetachedFromHand() {
        //Debug.Log("HandDetached: Lid");

        FixedJoint fixedJoint = Flask.GetComponent<FixedJoint>();
        if (fixedJoint == null) {
            counter = 1.0f;
            counter_flag = true;
        }
    }
    void OnTriggerEnter(Collider collider) {
        //Debug.Log("Lid_TriggerEnter:" + collider.gameObject.tag);
        if (collider.gameObject.tag == "JointArea") {
            joint_flag = true;
        }
    }
    void OnTriggerExit(Collider collider) {
        //Debug.Log("Lid_TriggerExit:" + collider.gameObject.tag);
        if (collider.gameObject.tag == "JointArea") {
            joint_flag = false;
        }
    }
}
