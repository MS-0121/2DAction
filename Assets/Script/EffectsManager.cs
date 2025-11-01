using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectsManager : MonoBehaviour
{
    Image DamageEffect;
    float red, green, blue, alpha;
    float _alpha;
    float speed = 0.03f;
    public bool damage = false;
    void Awake(){
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        DamageEffect = GameObject.Find("damageEffect").GetComponent<Image>();
        red = DamageEffect.color.r;
        green = DamageEffect.color.g;
        blue = DamageEffect.color.b;
        alpha = DamageEffect.color.a;
        _alpha = alpha;
    }

    // Update is called once per frame
    void Update()
    {
        if(damage) DamageProcess();
    }
    
    void DamageProcess(){
        DamageEffect.enabled = true;
        alpha -= speed;
        Alpha();
        if(alpha <= 0){
            damage = false;
            DamageEffect.enabled = false;
            alpha = _alpha;
        }
    }

    void Alpha(){
        DamageEffect.color = new Color(red, green , blue, alpha);
    }
}
