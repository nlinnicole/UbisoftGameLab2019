using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// attach to UI Text component (with the full text already there)

public class TypeWriter : MonoBehaviour
{
    public Text txt;

    public float speed;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

}
