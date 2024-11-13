using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;

    [SerializeField] public GameObject _deathScreen;
    [SerializeField] public int indexLevel;
    [SerializeField] public int currentIndex;

    public GameObject _hpBossCanvas;

    public Slider healthBar;

    [SerializeField] private GameObject _camera;

    private Magic _magic;

    private void Start()
    {
        healthBar.maxValue = _maxHealth;
        _health = _maxHealth;
        Time.timeScale = 1;
        _hpBossCanvas.SetActive(false);
    }

    private void Update()
    {
        if (_health <= 0)
        {
            _hpBossCanvas.SetActive(false);
            _deathScreen.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        healthBar.value = _health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public void Heal(int _heal)
    {
        if (_health < 100)
        {
            _health += _heal;
        }

        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(currentIndex);
        SceneManager.LoadSceneAsync(indexLevel);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ManaBoost")
        {
            Destroy(other.gameObject);
            _magic._maxMana += 10;
        }
    }
}
