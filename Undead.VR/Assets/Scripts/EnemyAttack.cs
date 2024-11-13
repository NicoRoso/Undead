using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private float _attackRange;
    [SerializeField] private int _damage;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip[] _swordClips;

    [SerializeField] private float _coolDown;

    private Player _player;

    private float _timer;

    public bool CanAttack { get; private set; }

    public float AttackRange => _attackRange;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _audio = GetComponent<AudioSource>();
    }


    private void Update()
    {
        UpdateCoolDown();
    }

    public void SoundSword()
    {
        AudioClip clipAttack = _swordClips[UnityEngine.Random.Range(0, _swordClips.Length)];
        _audio.PlayOneShot(clipAttack);
    }

    public void TryAttackPlayer()
    {
        _player.TakeDamage(_damage);
        SoundSword();

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
}