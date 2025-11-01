using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class DamageInterface : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}*/

public interface EnemyInterface{
    void AddDamage(float damage);
    void AkariDrop();
    void AkariDebuff(float Debuff);
    void ObjectDelete();
}