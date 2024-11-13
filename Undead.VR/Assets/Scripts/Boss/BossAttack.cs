using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage;

    [SerializeField] private BossAI _aiBoss;

    [SerializeField] private GameObject _magicBall;

    [SerializeField] private Transform _spawnBall;

    [SerializeField] private float _coolDown;
    [SerializeField] private float _coolDownMagic;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip[] _attackClips;

    [SerializeField] public bool _secondPhase;

    private Player _player;

    private float _timer;

    private float _timerMagic;

    public bool CanAttack { get; private set; }

    public bool CanAttackMagic { get; private set; }

    public float AttackRange => _attackRange;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _audio = GetComponent<AudioSource>();
        _secondPhase = false;
    }

    public void ChangePhase()
    {
        _secondPhase = true;
    }

    private void Update()
    {
        UpdateCoolDown();
    }

    public void SoundAttack()
    {
        AudioClip clipAttack = _attackClips[UnityEngine.Random.Range(0, _attackClips.Length)];
        _audio.PlayOneShot(clipAttack);
    }
    public void TryAttackPlayer()
    {
        _player.TakeDamage(_damage);
        SoundAttack();

        CanAttack = false;
    }

    public void SecondPhaseAttack()
    {
        SpawnBall();

        CanAttackMagic = false;
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

    private void UpdateMagicCoolDown()
    {
        if (CanAttackMagic)
        {
            return;
        }

        _timerMagic += Time.deltaTime;

        if (_timerMagic < _coolDownMagic)
        {
            return;
        }

        CanAttackMagic = true;

        _timerMagic = 0;
    }

    private void SpawnBall()
    {
        GameObject a1 = (GameObject)Instantiate(_magicBall, _spawnBall.transform.position, _spawnBall.transform.rotation);
    }
}