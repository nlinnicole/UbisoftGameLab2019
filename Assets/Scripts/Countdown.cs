using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private Image one;
    [SerializeField]
    private Image two;
    [SerializeField]
    private Image three;
    [SerializeField]
    private Image go;

    void Start()
    {
        StartCoroutine(Three());
        StartCoroutine(Two());
        StartCoroutine(One());
        StartCoroutine(Go());
        StartCoroutine(Done());
    }

    void Update()
    {

    
    }

    IEnumerator Three()
    {
        yield return new WaitForSeconds(1);
        three.enabled = true;
    }

    IEnumerator Two()
    {
        yield return new WaitForSeconds(2);
        three.enabled = false;
        two.enabled = true;
    }

    IEnumerator One()
    {
        yield return new WaitForSeconds(3);
        two.enabled = false;
        one.enabled = true;
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(4);
        one.enabled = false;
        go.enabled = true;
    }

    IEnumerator Done()
    {
        yield return new WaitForSeconds(5);
        go.enabled = false;
    }
}
