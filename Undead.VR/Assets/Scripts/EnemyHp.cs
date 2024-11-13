using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private int _hp;

    [SerializeField] private int _resistFire;
    [SerializeField] private int _resistIce;
    [SerializeField] private int _resistDeath;
    [SerializeField] private int _resistLighting;

    [SerializeField] private AIPath _aiPath;

    public GameObject _effectDead;

    [SerializeField] private EnemyAnimator _animator;

    public Slider healthBar;

    [Header("SoundClips")]
    [SerializeField] private AudioClip[] _hitClips;
    [SerializeField] private AudioClip[] _deathClips;
    [SerializeField] private AudioClip[] _swordClips;
    [SerializeField] private AudioClip[] _spellClips;

    [SerializeField] private AudioSource _audio;

    private bool _musicOff;

    [SerializeField] private float _deathDelay = 1f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            _hp -= 5;
            SoundHit();
            SoundSword();
        }   
    }

    public void FireBallDamage(int TakeDamage)
    {
        if (_resistFire >= TakeDamage)
        { 
            _resistFire = TakeDamage;
        }

        _hp -= TakeDamage - _resistFire;
        SoundHit();
        SoundSpell();
    }

    public void IceBallDamage(int TakeDamage)
    {
        if (_resistIce >= TakeDamage)
        { 
            _resistIce = TakeDamage; 
        }

        _hp -= TakeDamage - _resistIce;
        SoundHit();
        SoundSpell();
    }

    public void DeadBallDamage(int TakeDamage)
    {
        if (_resistDeath >= TakeDamage)
        {
            _resistDeath = TakeDamage;
        }

        _hp -= TakeDamage - _resistDeath;
        SoundHit();
        SoundSpell();
    }

    public void LightingArrowDamage(int TakeDamage)
    {   if (_resistLighting >= TakeDamage)
        {
            _resistLighting = TakeDamage;
        }

        _hp -= TakeDamage - _resistLighting;
        SoundHit();
        SoundSpell();
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _musicOff = true;
        _effectDead.SetActive(false);
        _aiPath = GetComponent<AIPath>();
    }

    public void SoundHit()
    {
        AudioClip clipAttack = _hitClips[UnityEngine.Random.Range(0, _hitClips.Length)];
        _audio.PlayOneShot(clipAttack);
    }

    public void SoundDeath()
    {
        AudioClip clipAttack = _deathClips[UnityEngine.Random.Range(0, _deathClips.Length)];
        _audio.PlayOneShot(clipAttack);
        _musicOff = false;
    }

    public void SoundSword()
    {
        AudioClip clipAttack = _swordClips[UnityEngine.Random.Range(0, _swordClips.Length)];
        _audio.PlayOneShot(clipAttack);
    }

    public void SoundSpell()
    {
        AudioClip clipAttack = _spellClips[UnityEngine.Random.Range(0, _spellClips.Length)];
        _audio.PlayOneShot(clipAttack);
    }



    private void Update()
    {
        if (_hp <= 0)
        {
            Death();
        }

        healthBar.value = _hp;
    }

    private void Death()
    {
        if (_musicOff)
        {
            SoundDeath();
        }
        _aiPath.maxSpeed = 0;
        _effectDead.SetActive(true);
        _animator.DeathAnim();
        Invoke("DestroyObject", _deathDelay);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
