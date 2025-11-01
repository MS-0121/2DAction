using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMove2 : MonoBehaviour
{
    //GameObject arm;
    //[SerializeField] 
    // Start is called before the first frame update
    void Start()
    {
        //arm = GameObject.GetChild(11).GetChild(0).GetChild(1);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //マウス座標所得
        mousePos = Vector3.Scale(mousePos, new Vector3(1,1,0)); //Z成分を0
        Vector3 dirVector = (mousePos - arm.Transform.position); //現在地からマウス座標までのベクトル所得
        quat = Quaternion.FromToRotation(Vector3.right, dirVector);
        thisTransform.rotation = quat;
        */
    }
}
