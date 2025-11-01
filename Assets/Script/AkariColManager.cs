using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkariColManager : MonoBehaviour
{
    [SerializeField] AkariManager AM;
    // Start is called before the first frame update
    void Start()
    {
        //AM = GameObject.Find("AkariObject").GetComponent<AkariManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "EventReceiv" && AM.drop){
            Debug.Log("灯を拾う");
            if(Input.GetKey(KeyCode.E)) AM.AkariPickUp();
        }
    }
}
