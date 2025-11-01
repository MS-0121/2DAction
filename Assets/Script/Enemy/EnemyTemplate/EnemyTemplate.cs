using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplate : MonoBehaviour, EnemyInterface
{
    [Header("キャラ詳細")]
    [Header("名前")]
    [SerializeField] string EnemyName;

    [Space(20),Header("基礎ステータス")]

    [Header("体力"),Range(1,2048)]
    [SerializeField] float BaseHP = 1; //敵の体力

    [Header("攻撃力"),Range(1,2048)]
    [SerializeField] float BaseATK = 1; //敵の攻撃力

    [Header("移動速度")]
    [SerializeField] float BaseSpeed = 1.0f;//敵の基本速度

    [Header("灯を落とす確率"),Range(0,100)]
    [SerializeField] float DropProbability = 40; //灯を落とす確率

    [Space(20),Header("現在のステータス")]
    [Header("移動速度")]
    [SerializeField] float speed; //敵の現在の速度
    [Header("体力")]
    [SerializeField] float HP; //現在の体力
    [Header("攻撃力")]
    [SerializeField] public float ATK; //現在の攻撃力

    [Space(40)]
    [Header("その他必要な項目")]
    [SerializeField] GameObject player;
    [SerializeField] Animator animator;
    [SerializeField] AkariManager AM;
    private Rigidbody2D RB2D;
    public bool stop = false;
    private float coefficient = 0.05f; //速度を正常化するための係数
    int key = 0; //向きを変えるための変数    
    
    //デバフ変数
    [SerializeField] float SpeedDebuff = 1;
    public float LightDebuff = 0;

    void Start()
    {
        EnemyName = "";
        HP = BaseHP;
        ATK = BaseATK;
        RB2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){

    }
    public void AddDamage(float damage){

    }

    public void AkariDebuff(float Debuff){

    }

    public void AkariDrop(){

    }

    public void ObjectDelete(){
        Destroy(gameObject);
    }
}
