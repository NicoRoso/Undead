using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private int indexLevel;
    public static int levelComplite = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (indexLevel == 0)
            {
                SceneManager.LoadScene(0);
            }
            else { SceneManager.LoadScene(indexLevel); }
        }
    }

}
