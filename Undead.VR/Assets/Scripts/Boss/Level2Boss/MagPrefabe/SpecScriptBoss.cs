using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecScriptBoss : MonoBehaviour
{
    [SerializeField] private float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(10);
        }
    }

    private void Start()
    {
        timer = Time.time;
    }

    private void Update()
    {
        Destroy(this.gameObject, timer);
    }
}
