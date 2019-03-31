using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCount : MonoBehaviour
{
    [SerializeField]
    private Image zero;
    [SerializeField]
    private Image one;
    [SerializeField]
    private Image two;
    [SerializeField]
    private Image three;
    [SerializeField]
    private Image four;
    [SerializeField]
    private Image five;
    [SerializeField]
    private Image six;
    [SerializeField]
    private Image seven;
    [SerializeField]
    private Image eight;
    [SerializeField]
    private Image nine;
    [SerializeField]
    private Image ten;

    void Start()
    {
        StartCoroutine(Ten());
        StartCoroutine(Nine());
        StartCoroutine(Eight());
        StartCoroutine(Seven());
        StartCoroutine(Six());
        StartCoroutine(Five());
        StartCoroutine(Four());
        StartCoroutine(Three());
        StartCoroutine(Two());
        StartCoroutine(One());
        StartCoroutine(Zero());
    }

    void Update()
    {


    }
    IEnumerator Ten()
    {
        yield return new WaitForSeconds(1);
        ten.enabled = true;
    }

    IEnumerator Nine()
    {
        yield return new WaitForSeconds(2);
        ten.enabled = false;
        nine.enabled = true;
    }

    IEnumerator Eight()
    {
        yield return new WaitForSeconds(3);
        nine.enabled = false;
        eight.enabled = true;
    }

    IEnumerator Seven()
    {
        yield return new WaitForSeconds(4);
        eight.enabled = false;
        seven.enabled = true;
    }

    IEnumerator Six()
    {
        yield return new WaitForSeconds(5);
        seven.enabled = false;
        six.enabled = true;
    }

    IEnumerator Five()
    {
        yield return new WaitForSeconds(6);
        six.enabled = false;
        five.enabled = true;
    }

    IEnumerator Four()
    {
        yield return new WaitForSeconds(7);
        five.enabled = false;
        four.enabled = true;
    }

    IEnumerator Three()
    {
        yield return new WaitForSeconds(8);
        four.enabled = false;
        three.enabled = true;
    }

    IEnumerator Two()
    {
        yield return new WaitForSeconds(9);
        three.enabled = false;
        two.enabled = true;
    }

    IEnumerator One()
    {
        yield return new WaitForSeconds(10);
        two.enabled = false;
        one.enabled = true;
    }

    IEnumerator Zero()
    {
        yield return new WaitForSeconds(11);
        one.enabled = false;
        zero.enabled = true;
    }


}
