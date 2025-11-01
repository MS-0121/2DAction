using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Vector3 direction;
    private Transform thisTransform;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        speed = 0.5f;
        Invoke("delete",3);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition;
        Vector3 addVector;
        addVector = new Vector3(direction.x * Time.deltaTime, direction.y * Time.deltaTime, 0);
        addVector.Normalize();
        newPosition = thisTransform.position + addVector * speed;
        transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
    }

    public void getVector(Vector3 from, Vector3 to){
        direction = new Vector3(to.x - from.x, to.y - from.y, to.z - from.z);
    }

    void delete(){
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){

        var DamageTarget = other.GetComponent<EnemyInterface>();
        if(DamageTarget != null){
            other.GetComponent<EnemyInterface>().AddDamage(1.0f);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Floor"){
            Destroy(this.gameObject);
        }
    }
}
