using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkariManager : MonoBehaviour
{
    //int
    public int akari; //現在の灯の値
    int akari_min, akari_max; //灯の最小値、最大値
    public bool drop; //灯を落としたかどうか
    Rigidbody2D RB2D; 
    [SerializeField] TextManager TM; //テキスト更新のスクリプト
    [SerializeField] GameObject Player; //プレイヤーオブジェクト
    private BoxCollider2D BoxCol; //灯本体の当たり判定
    int Counter = 0; //灯を落としている時、一秒ごとに値が減っていく。 
    int Counter_Down = 60; //落としている間、灯が減る間隔
    int Counter_ADown = 1; //落としてる間に減る値
    [SerializeField] float Debuff = 0;
    SpriteRenderer SR;
    [SerializeField] Sprite Akari_Bright;
    [SerializeField] Sprite Akari_Dark;
    // Start is called before the first frame update
    void Start()
    {
        akari = 100;
        akari_min = 0;
        akari_max = 150;
        RB2D = GetComponent<Rigidbody2D>();
        TM = GameObject.Find("TextController").GetComponent<TextManager>();
        Player = GameObject.Find("PlayerPivot1");
        TM.TextUpdate();
        BoxCol = GameObject.Find("AkariCol").GetComponent<BoxCollider2D>();
        SR = GameObject.Find("AkariCol").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(drop) {
            Counter++; 
            if(RB2D.velocity.y == 0) RB2D.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
        if(Counter == Counter_Down) AkariDown(Counter_ADown);
        if(!drop) { //デバフは移動速度-50%がデフォルト
            Debuff = -0.5f;
            this.transform.position = Player.transform.position; //プレイヤーに追従する
        }else Debuff = 0; //灯を落としている場合デバフ効果を消す
        if(akari > 80) SR.sprite = Akari_Bright;
        else SR.sprite = Akari_Dark;
    }

    void AkariDown(int x){
        Counter = 0;
        if(akari <= akari_min) return;
        if(akari >= akari_max) return;
        akari -= x;
        TM.TextUpdate();
    }

    public void AkariDrop(){ //灯を落とす
        RB2D.bodyType = RigidbodyType2D.Dynamic; //落とすと重力の影響を受けるようになる
        if(!drop)RB2D.AddForce(new Vector2(Random.Range(-5,5), 10), ForceMode2D.Impulse); //落とすと少し飛ぶ
        drop = true; 
        
    }

    public void AkariPickUp(){ //落とした灯を拾う
        drop = false;
        RB2D.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnTriggerStay2D(Collider2D other){ //敵が灯範囲内に入ったときの処理
        var AkariTarget = other.GetComponent<EnemyInterface>();

        if(AkariTarget != null){
            other.gameObject.GetComponent<EnemyInterface>().AkariDebuff(Debuff);
        }
    }

    void OnTriggerExit2D(Collider2D other){ //敵が灯範囲外に出たときの処理
        var AkariTarget = other.GetComponent<EnemyInterface>();

        if(AkariTarget != null){
            other.gameObject.GetComponent<EnemyInterface>().AkariDebuff(0);
        }
    }
}
