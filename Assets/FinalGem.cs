using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGem : MonoBehaviour
{
    public GameObject GemManager;

    private void Awake()
    {
        GemManager = GameObject.FindGameObjectWithTag("GemManager");
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (GemManager.GetComponent<GemManager>().Team1Gems > GemManager.GetComponent<GemManager>().Team2Gems)
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                SceneManager.LoadScene(4);
            }
        }
        
    }
}
