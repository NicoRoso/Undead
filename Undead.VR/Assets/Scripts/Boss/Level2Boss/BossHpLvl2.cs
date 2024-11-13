using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHpLvl2 : MonoBehaviour
{
    [SerializeField] public int _hp;

    [SerializeField] public bool _bossDead;

    public GameObject _EndLevel;

    public Slider healthBar;

    public GameObject _hpPlayerCanvas;
    public GameObject _secondPhaseSince;

    [SerializeField] private AnimatorBoss _animator;

    [SerializeField] public int _resistFire;
    [SerializeField] public int _resistIce;
    [SerializeField] public int _resistDeath;
    [SerializeField] public int _resistLighting;

    public TextMeshProUGUI _timerText;
    private bool _timerIsRunning;
    private float _timerTime = 0f;
    public GameObject _canvasTimer;

    [Header("SoundClips")]
    [SerializeField] private AudioClip[] _hitClips;
    [SerializeField] private AudioClip[] _deathClips;
    [SerializeField] private AudioClip[] _swordClips;
    [SerializeField] private AudioClip[] _spellClips;
    [SerializeField] private AudioClip[] _SecondPhaseLaugh;

    [SerializeField] private AudioSource _audio;

    private bool _musicOff;
    private bool _musicOff2;

    [SerializeField] private float _deathDelay = 1f;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            _animator.HitHp();
            _hp -= 5;
        }
    }

    public void FireBallDamage(int TakeDamage)
    {
        if (_resistFire >= TakeDamage)
        {
            _resistFire = TakeDamage;
        }
        _animator.HitHp();
        _hp -= TakeDamage - _resistFire;
    }

    public void IceBallDamage(int TakeDamage)
    {
        if (_resistIce >= TakeDamage)
        {
            _resistIce = TakeDamage;
        }
        _animator.HitHp();
        _hp -= TakeDamage - _resistIce;
    }

    public void DeadBallDamage(int TakeDamage)
    {
        if (_resistDeath >= TakeDamage)
        {
            _resistDeath = TakeDamage;
        }
        _animator.HitHp();
        _hp -= TakeDamage - _resistDeath;
    }

    public void LightingArrowDamage(int TakeDamage)
    {
        if (_resistLighting >= TakeDamage)
        {
            _resistLighting = TakeDamage;
        }
        _animator.HitHp();
        _hp -= TakeDamage - _resistLighting;
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _musicOff = true;
        _musicOff2 = true;
        _hpPlayerCanvas.SetActive(true);
        _secondPhaseSince.SetActive(false);
        _bossDead = false;
        healthBar.maxValue = _hp;
        _EndLevel.SetActive(false);
        _timerText.text = "Время: " + _timerTime.ToString("F2");
        _timerIsRunning = true;
        _canvasTimer.SetActive(true);
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
        AudioClip clipAttack = _swordClips[UnityEngine.Random.Range(0, _hitClips.Length)];
        _audio.PlayOneShot(clipAttack);
    }

    public void SoundSpell()
    {
        AudioClip clipAttack = _spellClips[UnityEngine.Random.Range(0, _hitClips.Length)];
        _audio.PlayOneShot(clipAttack);
    }
    public void Laugh()
    {
        AudioClip clipAttack = _SecondPhaseLaugh[UnityEngine.Random.Range(0, _SecondPhaseLaugh.Length)];
        _audio.PlayOneShot(clipAttack);
    }
    private void Update()
    {
        if (_hp <= 0)
        {
            _hpPlayerCanvas.SetActive(false);
            _timerIsRunning = false;
            _EndLevel.SetActive(true);
            Death();
        }

        if (_timerIsRunning)
        {
            _timerTime += Time.deltaTime;
            _timerText.text = "Время: " + _timerTime.ToString("F2");
        }

        healthBar.value = _hp;

        if (_hp <= 50 && _hp > 0)
        {
            if (_musicOff2)
            {
                Laugh();
                _musicOff2 = false;
            }
            _secondPhaseSince.SetActive(true);
            gameObject.GetComponent<BossLvl2>().ChangePhase();
        }
    }

    private void Death()
    {
        if (_musicOff)
        {
            SoundDeath();
        }
        _bossDead = true;
        _animator.DeathHp();
        Invoke("DestroyObject", _deathDelay);
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
