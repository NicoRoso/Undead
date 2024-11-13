using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHp : MonoBehaviour
{
    [SerializeField] public int _hp;

    public bool _bossDead = false;

    private bool _onlyOne;

    public GameObject _hpPlayerCanvas;

    public GameObject _EndWall;

    public Slider healthBar;

    [SerializeField] private BossAnimator _animator;

    public TextMeshProUGUI _timerText;
    private bool _timerIsRunning;
    private float _timerTime = 0f;
    public GameObject _canvasTimer;

    [SerializeField] public int _resistFire;
    [SerializeField] public int _resistIce;
    [SerializeField] public int _resistDeath;
    [SerializeField] public int _resistLighting;

    [Header("SoundClips")]
    [SerializeField] private AudioClip[] _hitClips;
    [SerializeField] private AudioClip[] _deathClips;
    [SerializeField] private AudioClip[] _swordClips;
    [SerializeField] private AudioClip[] _spellClips;
    [SerializeField] private AudioClip[] _shoutClips;

    [SerializeField] private AudioSource _audio;

    [SerializeField] private BossAnimator _enemyAnimator;

    private bool _musicOff;
    private bool _musicOff2;

    [SerializeField] private float _deathDelay = 1f;

    public void SoundShout()
    {
        AudioClip clipAttack = _shoutClips[UnityEngine.Random.Range(0, _shoutClips.Length)];
        _audio.PlayOneShot(clipAttack);
        _musicOff2 = true;
    }

    private void SecondPhase()
    {
        if (_musicOff2)
        {
            SoundShout();
        }
        _enemyAnimator.ShoutHp();
        _resistFire += 1;
        _resistIce += 1;
        _resistDeath += 1;
        _resistLighting += 1;
        gameObject.GetComponent<BossAttack>().ChangePhase();
        _onlyOne = false;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            _hp -= 5;
            SoundSword();
        }
    }

    public void FireBallDamage(int TakeDamage)
    {
        if (_resistFire >= TakeDamage)
        {
            _resistFire = TakeDamage;
            SoundSpell();
        }

        _hp -= TakeDamage - _resistFire;
    }

    public void IceBallDamage(int TakeDamage)
    {
        if (_resistIce >= TakeDamage)
        {
            _resistIce = TakeDamage;
            SoundSpell();
        }

        _hp -= TakeDamage - _resistIce;
    }

    public void DeadBallDamage(int TakeDamage)
    {
        if (_resistDeath >= TakeDamage)
        {
            _resistDeath = TakeDamage;
            SoundSpell();
        }

        _hp -= TakeDamage - _resistDeath;
    }

    public void LightingArrowDamage(int TakeDamage)
    {
        if (_resistLighting >= TakeDamage)
        {
            _resistLighting = TakeDamage;
            SoundSpell();
        }

        _hp -= TakeDamage - _resistLighting;
    }

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _musicOff = true;
        _hpPlayerCanvas.SetActive(true);
        _onlyOne = true;
        _EndWall.SetActive(false);
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

    private void Update()
    {
        if (_timerIsRunning)
        {
            _timerTime += Time.deltaTime;
            _timerText.text = "Время: " + _timerTime.ToString("F2");
        }

        if (_hp <= 50 && _hp > 0)
        {

            if (_onlyOne)
            {
                SecondPhase();
            }

            gameObject.GetComponent<BossAttack>().ChangePhase();
        }


        if (_hp <= 0)
        {
            _hpPlayerCanvas.SetActive(false);
            _timerIsRunning = false;
            _EndWall.SetActive(true);
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
        _bossDead = true;
        _animator.DeathHp();
        Invoke("DestroyObject", _deathDelay);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
