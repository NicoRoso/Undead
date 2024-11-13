using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : MonoBehaviour
{

    [SerializeField] private float _magicLife;
    [SerializeField] private float _speed;
    private void Awake()
    {
        Destroy(gameObject, _magicLife);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<EnemyAI>().ChangeSpeed();
            collision.gameObject.GetComponent<EnemyHp>().IceBallDamage(3);
            
        }

        if (collision.gameObject.tag == "BossLvl1")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<EnemyHp>().IceBallDamage(3);
        }

        if (collision.gameObject.tag == "BossLvl2")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<EnemyHp>().IceBallDamage(3);
        }
    }
}
