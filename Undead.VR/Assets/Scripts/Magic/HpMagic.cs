using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpMagic : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<Player>().Heal(20);
        }
    }
}
