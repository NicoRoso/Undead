using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallBoss : MonoBehaviour
{

    [SerializeField] private GameObject _boss;

    [SerializeField] private GameObject _wall;

    public GameObject _boost;


    [SerializeField] private int indexLevel;

    [SerializeField] private AudioSource _throwWall;
    [SerializeField] private AudioClip _throwWallClip;

    private void Start()
    {
        _boss.SetActive(false);
        _wall.SetActive(false);
        _throwWall = GetComponent<AudioSource>();

        if (EndLevel.levelComplite == 0)
        {
            _boost.SetActive(false);
        }
        else
        {
            _boost.SetActive(true);
        }
    }

    private void Update()
    {

    }

    private void Sound()
    {
        _throwWall.PlayOneShot(_throwWallClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _boss.SetActive(true);
            _wall.SetActive(true);
            Sound();

        }
    }
}
