using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour, PInterface
{

    [Header("基礎ステータス")]
    [SerializeField] float BaseHP = 100;
    [SerializeField] float BaseMoveSpeed = 10.0f;
    int SlashCounter = 0;
    public int key = 1;
    int KnockCT = 0;
    int JumpPower = 25;

    [Space(20),Header("現在のステータス")]
    [SerializeField] float HP;
    [SerializeField] float MoveSpeed;

    //bool
    [Space(20),Header("フラグ")]
    [SerializeField] bool Knock = false;
    [SerializeField] public bool Slash = false;
    [SerializeField] bool Move = false;
    [SerializeField] bool arm_dis = true;
    [SerializeField] public bool skill = false;


    

    Vector2 Moving, Slashing, Knocking;
    float SlashSpeed;
    [Space(40),Header("その他必要な項目")]    
    //Script
    [Header("Script")]
    [SerializeField] ArmMove arm;
    [SerializeField] EffectsManager EM;
    [SerializeField] AkariManager AM;

    //Object類
    private CapsuleCollider2D CapCol;
    Enemy_1Move E1;
    [Header("Object")]
    [SerializeField] private Rigidbody2D RB2D;
    [SerializeField] Animator animator;
    [SerializeField] GameObject PlayerPivot;
    [SerializeField] SpriteRenderer Left_Up, Left_Down, Right_Up, Right_Down, Left_Pivot1, Left_Pivot2, Right_Pivot1, Right_Pivot2;

    //String型
    //string Enemy_1Name = "Enemy_1";

    void Start()
    {
        CapCol = GetComponent<CapsuleCollider2D>();
        MoveSpeed = BaseMoveSpeed;
        HP = BaseHP;
        RB2D = PlayerPivot.GetComponent<Rigidbody2D>();
        arm_display(); //アニメーション用の腕は初期非表示
    }

    void Update(){
        //リスポーン
        if(Input.GetKey(KeyCode.R)) PlayerPivot.transform.position = new Vector2(0,5.0f);
        //左右移動
        if(!skill){
            if(!Knock){
                if(!Slash){
                    //移動キー同時押しの際の処理 || 移動キー入力なしの処理
                    if((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))){ 
                        this.animator.SetBool("dashBool", false);
                        this.animator.SetBool("backdashBool", false);
                        Move = false;
                    }else if(Input.GetKey(KeyCode.A)){
                        if(arm.key == -1){
                            this.animator.SetBool("backdashBool", true);
                            this.animator.SetBool("dashBool", false);
                            Debug.Log("後ろ");
                        }else{
                            this.animator.SetBool("backdashBool", false);
                            this.animator.SetBool("dashBool", true);
                            Debug.Log("前");
                        }
                        key = -1;
                        Move = true;
                        Moving = new Vector2(MoveSpeed,0);
                    }else if(Input.GetKey(KeyCode.D)){
                        if(arm.key == 1){
                            this.animator.SetBool("backdashBool", true);
                            this.animator.SetBool("dashBool", false);
                        }else{
                            this.animator.SetBool("backdashBool", false);
                            this.animator.SetBool("dashBool", true);
                        }
                        key = 1;
                        Move = true;
                        Moving = new Vector2(MoveSpeed,0);
                    }
                    //スキル
                    if(Input.GetKeyDown(KeyCode.E)) AkariSkill();
                }
                //ジャンプ
                if(Input.GetKey(KeyCode.Space) && RB2D.velocity.y == 0 && !Slash) Jump(JumpPower);
                //ジャンプアニメーション遷移
                if(RB2D.velocity.y > 0.1f){
                    this.animator.SetBool("jumpBool", true);
                }else if(RB2D.velocity.y < 0){
                    this.animator.SetBool("jumpBool", false);
                    this.animator.SetBool("downBool", true);
                }else{
                    this.animator.SetBool("downBool", false);
                    this.animator.SetBool("jumpBool", false);
                }
                //近接攻撃
                if(Input.GetMouseButtonDown(1) && !Slash){
                    Slash = true;
                    Move = false;
                    Slashing = new Vector2(SlashSpeed,0);
                }
                SlashAttack();
            }
        }
        
        if(KnockCT > 0) {
            KnockCT--;
            Knock = true;
            Move = false;
        }else{
            Knock = false;
            //E1.stop = false; //敵移動停止解除
        }
        SceneChange();
    }
    void FixedUpdate(){
        if(!Knock) RB2D.velocity = new Vector2(0, RB2D.velocity.y); //通常は静止状態
        if(!Knock){
            if(Move) RB2D.velocity = new Vector2(Moving.x * key, RB2D.velocity.y); //移動
            if(Slash) RB2D.velocity = new Vector2(Slashing.x * key, RB2D.velocity.y); //突進
            if(skill) RB2D.velocity = new Vector2(0, RB2D.velocity.y);
        }
    }
    public void arm_display(){
        //関数実行時に表示を切り替える(初期はスクリプトで動かす腕を表示)
        if(arm_dis){
            //アニメーション用の腕の非表示、スクリプトで動かす腕の表示
            //初期の実行はこっち
            Left_Up.color = new Color(255, 255, 255, 0);
            Left_Down.color = new Color(255, 255, 255, 0);
            Left_Pivot1.color = Left_Pivot2.color = new Color(255, 255, 255, 255);
            Right_Up.color = new Color(255, 255, 255, 0);
            Right_Down.color = new Color(255, 255, 255, 0);
            Right_Pivot1.color = Right_Pivot2.color = new Color(255, 255, 255, 255);
            arm_dis = false;
            this.animator.SetBool("skillBool", false);
        }else{
            //スクリプトで動かす腕の非表示、アニメーション用の腕の表示
            Left_Up.color = new Color(255, 255, 255, 255);
            Left_Down.color = new Color(255, 255, 255, 255);
            Left_Pivot1.color = Left_Pivot2.color = new Color(255, 255, 255, 0);
            Right_Up.color = new Color(255, 255, 255, 255);
            Right_Down.color = new Color(255, 255, 255, 255);
            Right_Pivot1.color = Right_Pivot2.color = new Color(255, 255, 255, 0);
            arm_dis = true;
        }
    }

    void AkariSkill(){
        FreezePosY(0);
        skill = true;
        arm_display();
        this.animator.SetBool("skillBool", true);
    }

    void Jump(float _JumpPower){
        RB2D.velocity = new Vector2(RB2D.velocity.x, _JumpPower);
    }

    void SlashAttack(){
        //突進攻撃
        if(Slash){
            CapCol.isTrigger = true; //突進中は敵をすり抜ける
            FreezePosY(0); //突進中のY軸移動を停止
            SlashCounter++;

            if(arm.key == -1){
                if(key == -1){
                    this.animator.SetBool("backslashBool", true);
                    Debug.Log("後key-1");
                }else{
                    this.animator.SetBool("frontslashBool", true);
                    Debug.Log("前key-1");
                }
            }else{
                if(key == -1){
                    this.animator.SetBool("frontslashBool", true);
                    Debug.Log("前key1");
                }else{
                    this.animator.SetBool("backslashBool", true);
                    Debug.Log("後key1");
                }
            }

        }
        if(SlashCounter == 10){
            RB2D.velocity = new Vector2(0,0);
            Invoke("SSCancel",0.1f); //Y軸移動停止を解除
            SlashCounter = 0;
        }
    }
    //SlashStopCancel
    void SSCancel(){
        CapCol.isTrigger = false; //すり抜け解除
        FreezePosY(1); //y軸移動停止解除
        this.animator.SetBool("backslashBool", false);
        this.animator.SetBool("frontslashBool", false);
        RB2D.velocity = new Vector2(0, -0.1f);
        Slash = false;
    }

    public void FreezePosY(int x){
        //xが0でy軸移動停止
        if(x == 0) RB2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //xが1でy軸移動停止解除
        else if(x == 1) RB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Knockback(Vector3 a){
        int KnockKey;
        KnockCT = 20;
        if(a.x >= PlayerPivot.transform.position.x){ //自分が敵より左側にいる場合
            KnockKey = -1; //左に吹っ飛ぶ
        }else{
            KnockKey = 1; //右に吹っ飛ぶ
        }
        float KnockbackPower = 10;
        Knocking = new Vector2(KnockbackPower * KnockKey, KnockbackPower);
        RB2D.velocity = new Vector2(0,0);
        RB2D.AddForce(Knocking, ForceMode2D.Impulse);
        E1.AkariDrop();
    }

    void AddDamage(float ATK){
        HP -= ATK;
    }

    void SceneChange(){
        if(HP <= 0) SceneManager.LoadScene("ResultScene");
    }

     //敵、敵の攻撃に当たった時の処理
    void OnCollisionStay2D(Collision2D other){
        var EnemyTarget = other.gameObject.GetComponent<EnemyInterface>();
        if(EnemyTarget != null && KnockCT == 0){
            if(!Slash) {
                //other.gameObject.GetComponent<EnemyInterface>().
                //E1 = other.gameObject.GetComponent<EnemyInterface>();
                AddDamage(E1.ATK);
                E1.stop = true; //敵に当たったら敵移動停止
                EM.damage = true;
                Knockback(other.gameObject.transform.position);
            }
        }
    }
}
