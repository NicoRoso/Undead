using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    [SerializeField] private float _arrowlife;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 40;
    }

    private void Awake()
    {
        Destroy(gameObject, _arrowlife);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Player>().TakeDamage(5);
        }
    }
}