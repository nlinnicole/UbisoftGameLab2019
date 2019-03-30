using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// attach to UI Text component (with the full text already there)

public class TypeWriter : MonoBehaviour
{

    public float speed;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void Start()
    {
        Destroy(gameObject, 15);
    }

}
