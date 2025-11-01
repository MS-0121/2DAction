using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    PlayerMove Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerPivot1").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void skillFin(){
        Player.arm_display();
        Player.FreezePosY(1);
        Player.skill = false;
        Debug.Log("発動");
    }
}
