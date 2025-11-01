using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn1 : MonoBehaviour
{
    [SerializeField] GameObject Enemy_1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            GameObject cloneEnemy_1 = Instantiate(Enemy_1, new Vector3(0,0,0), Quaternion.identity);
            cloneEnemy_1.name = cloneEnemy_1.name.Replace("(Clone)","");
        }
    }
}
