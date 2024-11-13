using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class Magic : MonoBehaviour
{

    [SerializeField] private GameObject _spawnPos;
    [SerializeField] private InputActionProperty _activeAction;
    [SerializeField] private InputActionProperty _gripAction;

    [SerializeField] private int _mana;
    public int _maxMana;

    public Slider manaBar;

    private bool _canSwitch;
    private bool _canSpawn = true;
    private float _lastSwitchTime = 0f;
    private float _lastSwitchInterval = 1f;
    private float _lastSpawnTime = 0f;
    [SerializeField] private float _spawnInterval = 5f;

    private float _scorInterval = 1f;
    private float _lastScoreTime = 0f;


    [Header("MagicSkils")]
    public List<GameObject> allMagic;

    private int _currentMagicSkille;


    // Start is called before the first frame update
    void Start()
    {
        _maxMana = 10;
        manaBar.maxValue = _maxMana;
        _mana = _maxMana;
        _canSwitch = true;
        _canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_activeAction.action.ReadValue<float>() != 0)
        {
            SpawnMagic();
        }

        if (_gripAction.action.ReadValue<float>() != 0)
        {
            SwitchMagic();
        }

            if (Time.time - _lastScoreTime >= _scorInterval)
        {
            if (_mana < _maxMana)
            {
                _mana++;
            }
            _lastScoreTime = Time.time;
        }

        if (!_canSpawn && Time.time - _lastSpawnTime >= _spawnInterval)
        {
            _canSpawn = true;
        }
        if (!_canSwitch && Time.time - _lastSwitchTime >= _lastSwitchInterval)
        {
            _canSwitch = true;
        }

        manaBar.value = _mana;
    }

    public void SwitchMagic()
    {
        if (_canSwitch)
        {
            _currentMagicSkille++;
            if (_currentMagicSkille >= allMagic.Count)
            {
                _currentMagicSkille = 0;
            }
            _canSwitch = false;
            _lastSwitchTime = Time.time;
        }
    }

    public void SpawnMagic()
    {
        if (_mana >= 3 && _canSpawn)
        {
            GameObject a1 = (GameObject)Instantiate(allMagic[_currentMagicSkille], _spawnPos.transform.position, _spawnPos.transform.rotation);
            _mana -= 3;
            _canSpawn = false;
            _lastSpawnTime = Time.time;
        }
    }
}
