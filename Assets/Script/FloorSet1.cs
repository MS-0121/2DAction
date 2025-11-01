using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSet1 : MonoBehaviour
{
    [SerializeField] GameObject[] floor = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i < 8;i++){
            Instantiate(floor[i%2],new Vector3(10.0f * i - 10.0f, -5.0f, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
