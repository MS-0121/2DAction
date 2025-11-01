using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    int Cooltime = 0;
    int BulletCooltime = 15;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && Cooltime == 0){
            Vector3 initial;
            initial = InitialPosition(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            initial.Normalize();
            //生成するもの、スクリプトをアタッチしたオブジェクトの位置、回転していないQuaternion
            GameObject g = Instantiate(bullet, transform.position + initial * 1.8f, Quaternion.identity); 
            g.GetComponent<BulletMove>().getVector(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Cooltime = BulletCooltime;
        }
        if(Cooltime > 0)Cooltime--;
    }
    Vector3 InitialPosition(Vector3 from, Vector3 to){
        return new Vector3(to.x - from.x, to.y - from.y, 0);
    }

    
}
