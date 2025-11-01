using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMove : MonoBehaviour
{
    [SerializeField] Transform thisTransform;
    //[SerializeField] Transform WeaponTransform;
    Quaternion quat;
    PlayerMove Player;
    [SerializeField] GameObject PlayerPivot;
    public int key;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //PlayerPivot = GameObject.Find("PlayerPivot");
        PlayerPivot = GameObject.Find("PlayerPivot1");
        //Player = GameObject.Find("BoneTest").GetComponent<PlayerMove>();
        Player = PlayerPivot.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //マウス座標所得
        mousePos = Vector3.Scale(mousePos, new Vector3(1,1,0)); //Z成分を0
        Vector3 dirVector = (mousePos - thisTransform.position); //現在地からマウス座標までのベクトル所得
        quat = Quaternion.FromToRotation(Vector3.right, dirVector * key);
        thisTransform.rotation = quat;
        if(mousePos.x > PlayerPivot.transform.position.x){
            if(!Player.Slash) PlayerPivot.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            key = -1;
        }else{
            if(!Player.Slash) PlayerPivot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            key = 1;
        }
    }
}
