using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1Move : MonoBehaviour, EnemyInterface
{
    [Header("キャラの詳細")]
    [Header("名前")]
    [SerializeField] string EnemyName;

    [Space(20),Header("基礎ステータス")]
    [Header("体力"),Range(1,2048)]
    [SerializeField] float BaseHP = 3; //敵の体力
    [Header("攻撃力"),Range(1,2048)]
    [SerializeField] float BaseATK = 5;
    [Header("移動速度")]
    [SerializeField] float BaseSpeed = 1.0f;//敵の基本速度
    [Header("灯を落とす確率"),Range(0,100)]
    [SerializeField] float DropProbability = 40; //灯を落とす確率

    [Space(20),Header("現在のステータス")]
    [Header("移動速度")]
    [SerializeField] float speed; //敵の現在の速度
    [Header("体力")]
    [SerializeField] float HP;
    [Header("攻撃力")]
    [SerializeField] public float ATK;

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
    // Start is called before the first frame update
    void Start()
    {
        EnemyName = "未発達個体";
        HP = BaseHP;
        ATK = BaseATK;
        RB2D = GetComponent<Rigidbody2D>();
        KeyGet();
    }

    void Update()
    {
        if(HP <= 0) ObjectDelete();
        speed = SpeedProcess();
        if(!stop) transform.Translate(speed, 0, 0);
        if(stop) {
            Invoke("StopCancel", 0.4f);
            KeyGet();
        }
    }

    void FixedUpdate(){
        RB2D.velocity = new Vector2(0, RB2D.velocity.y);
    }

    void KeyGet(){
        if(player.transform.position.x > this.transform.position.x) key = 1; //右移動
        else key = -1; //左移動
    }

    public void AddDamage(float damage){
        HP -= damage;
        //Debug.Log(damage + "ダメージを与えた");
    }

    void StopCancel(){
        stop = false;
    }

    float SpeedProcess(){ //スピードの計算
        return (BaseSpeed * buff() * key) * coefficient;
    }

    float buff(){ //スピードバフ/デバフの計算
        SpeedDebuff = 1;
        return SpeedDebuff = SpeedDebuff + (LightDebuff);
    }

    public void AkariDebuff(float Debuff){
        LightDebuff = Debuff;
    }

    public void AkariDrop(){
        int a = Random.Range(0,100);
        if(a < DropProbability) {
            Debug.Log("落とした：" + a + " < " + DropProbability);
            AM.AkariDrop();
        }
    }

    public void ObjectDelete(){
        Destroy(gameObject);
    }
}
