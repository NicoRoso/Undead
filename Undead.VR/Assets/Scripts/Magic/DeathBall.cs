using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBall : MonoBehaviour
{

    [SerializeField] private float _magicLife;
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _player;

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
            _player.GetComponent<Player>().TakeDamage(5);
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<EnemyHp>().DeadBallDamage(9);
        }

        if (collision.gameObject.tag == "BossLvl1")
        {
            _player.GetComponent<Player>().TakeDamage(5);
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<BossHp>().DeadBallDamage(9);
        }

        if (collision.gameObject.tag == "BossLvl2")
        {
            _player.GetComponent<Player>().TakeDamage(5);
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<BossHpLvl2>().DeadBallDamage(9);
        }
    }
}
