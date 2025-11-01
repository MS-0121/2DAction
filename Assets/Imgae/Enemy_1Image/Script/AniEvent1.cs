using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEvent1 : MonoBehaviour
{
    Animator animator;
    [SerializeField] Enemy_1Move EM;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //EM = GameObject.Find("Enemy_1Pivot").GetComponent<Enemy_1Move>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoolChanger(){
        this.animator.SetBool("WalkBool", false);
        EM.stop = true;
        Invoke("stopCancel", 0.4f);
    }

    void stopCancel(){
        this.animator.SetBool("WalkBool", true);
    }
}
