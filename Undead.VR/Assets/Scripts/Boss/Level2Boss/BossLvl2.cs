using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossLvl2 : MonoBehaviour
{

    public GameObject _player;
    public GameObject _specObject;

    public List<GameObject> _magicAttack;

    [SerializeField] private AnimatorBoss _bossAnimator;

    private int _magicInt;

    public Transform _spawn1;

    public Transform _specSpawn;

    private float _lastSpawnTime = 0f;
    private float _lastSpawn2Time = 0f;

    private bool _canSpawn = true;

    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private float _spawnInterval2 = 10f;

    [SerializeField] public bool _secondPhase;

    private BossHpLvl2 _hpLvl2;

    private bool _canSpawnPhase2;

    void Start()
    {
        _canSpawn = true;
    }

    void Update()
    {
        transform.LookAt(_player.transform.position);
        Attack();
        RandomSkille();

        if (!_canSpawn && Time.time - _lastSpawnTime >= _spawnInterval)
        {
            _canSpawn = true;
        }

        if (!_canSpawnPhase2 && Time.time - _lastSpawn2Time >= _spawnInterval2)
        {
            _canSpawnPhase2 = true;
        }

        if (_secondPhase)
        {
            SpecAttack();
        }
    }

    public void ChangePhase()
    {
        _secondPhase = true;
    }

    private void RandomSkille()
    {
        _magicInt = Random.Range(0, 1);
    }

    private void Attack()
    {
        if (_canSpawn)
        {
            GameObject a1 = (GameObject)Instantiate(_magicAttack[0], _spawn1.transform.position, _spawn1.transform.rotation);
            _canSpawn = false;
            _lastSpawnTime = Time.time;
            _bossAnimator.PlayerAttack();
        }
    }

    private void SpecAttack()
    {
        if (_secondPhase && _canSpawnPhase2)
        {
            GameObject a1 = (GameObject)Instantiate(_specObject, _specSpawn.transform.position, _specSpawn.transform.rotation);
            _canSpawnPhase2 = false;
            _lastSpawn2Time = Time.time;
        }
    }

    
}
