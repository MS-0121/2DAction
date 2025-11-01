using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject Target;
    Camera Cam;
    Transform Tf;
    [SerializeField] GameObject LeftEdge;
    [SerializeField] GameObject RightEdge;
    public bool ZoomFlag = false;
    public float pos_y = 0; //初期値

    // Start is called before the first frame update
    void Start()
    {
        //Target = GameObject.Find("PlayerPivot");
        Target = GameObject.Find("PlayerPivot1");
        Cam = this.gameObject.GetComponent<Camera>();
        Tf = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate(){
        //通常時
        this.transform.position = new Vector3(Target.transform.position.x, pos_y, this.transform.position.z);
        //this.transform.position = Target.transform.position;
        //左端にいるときの処理
        if (this.transform.position.x <= LeftEdge.transform.position.x)
        {
            this.transform.position = new Vector3(LeftEdge.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        //右端にいるときの処理
        else if (this.transform.position.x >= RightEdge.transform.position.x)
        {
            this.transform.position = new Vector3(RightEdge.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
        Zoom();
    }

    void Zoom(){
        if(!ZoomFlag){
            if(Cam.orthographicSize < 5)Cam.orthographicSize += 0.1f; //ズーム解除
            if(Cam.orthographicSize > 5) Cam.orthographicSize = 5;
            if(pos_y < 0) pos_y += 0.1f; //カメラのyを戻す
            if(pos_y > 0) pos_y = 0;
        }
        if(ZoomFlag){
            if(Cam.orthographicSize > 2) Cam.orthographicSize -= 0.1f; //ズームする
            if(Cam.orthographicSize < 2) Cam.orthographicSize = 2;
            if(pos_y >= Target.transform.position.y) pos_y -= 0.1f; //カメラのyをプレイヤーの位置に
            if(pos_y < Target.transform.position.y) pos_y = Target.transform.position.y;
        }
    }
}
