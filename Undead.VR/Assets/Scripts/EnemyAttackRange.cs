using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{

    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage;

    [SerializeField] public GameObject _arrowEnemy;

    [SerializeField] private Transform _spawnPos;

    [SerializeField] private float _coolDown;

    private Player _player;

    private float _timer;

    public bool CanAttack { get; private set; }

    public float AttackRange => _attackRange;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }


    private void Update()
    {
        UpdateCoolDown();

    }

    public void TryAttackPlayer()
    {
        SpawnArrow();

        CanAttack = false;
    }

    private void UpdateCoolDown()
    {
        if (CanAttack)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_timer < _coolDown)
        {
            return;
        }

        CanAttack = true;

        _timer = 0;
    }

    private void SpawnArrow()
    {
        GameObject a1 = (GameObject)Instantiate(_arrowEnemy, _spawnPos.transform.position, _spawnPos.transform.rotation);
    }
}