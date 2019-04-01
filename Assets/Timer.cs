using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool StartTiming;
    public float timer;
    public Text TimerText;
    // Update is called once per frame

    private void Start()
    {
        TimerText = gameObject.GetComponent<Text>();
    }


    void Update()
    {
        if (StartTiming)
        {
            timer += Time.deltaTime;
            TimerText.text = Mathf.Round(timer).ToString();
        }
    }
}
