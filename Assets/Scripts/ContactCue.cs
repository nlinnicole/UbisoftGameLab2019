using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactCue : MonoBehaviour
{
    public Renderer[] renderers;
    public Color colorA;
    public Color colorB;

    private Color constA;
    private Color constB;

    private float interpolation = 0f;
    public float speed = 0.1f;

    private bool glowing = false;
    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        colorA = renderers[0].materials[0].color;
        colorB = renderers[0].materials[1].color;

        constA = new Color(colorA.r, colorA.g, colorA.b, colorA.a);
        constB = new Color(colorB.r, colorB.g, colorB.b, colorB.a);
    }

    // Update is called once per frame
    void Update()
    {
        //When the box matches the button, lerp the colors to indicate success.
        if (GetComponent<MultiButton>().getPressed()){        
          if (interpolation < 1f){
            colorA = Color.Lerp(constA, constB, interpolation );
            colorB = Color.Lerp(constB, constA, interpolation);
            for (int i = 0; i < renderers.Length; i++){
              renderers[i].materials[0].color = colorA;
              renderers[i].materials[1].color = colorB;
            }
            interpolation += Time.deltaTime * speed;
          } else {
            //Once we finish interpolating we set the glowing factor.
            if (!glowing){
              glowing = true;
              for (int i = 0; i < renderers.Length; i++){
                //Adding glow to main color
                renderers[i].materials[0].EnableKeyword("_EMISSION");
                renderers[i].materials[0].globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

                Color glowing = renderers[0].materials[1].GetColor("_EmissionColor");
                renderers[i].materials[0].SetColor("_EmissionColor", glowing);

                //Removing glow from secondary color
                renderers[i].materials[1].DisableKeyword("_EMISSION");
              }
            }
          }
        } else {
          if (interpolation > 0f){
            colorA = Color.Lerp(constB, constA, interpolation );
            colorB = Color.Lerp(constA, constB, interpolation);
            for (int i = 0; i < renderers.Length; i++){
              renderers[i].materials[0].color = colorB;
              renderers[i].materials[1].color = colorA;
            }
            interpolation -= Time.deltaTime * speed;
          } else {
            //Turn glowing off.
            if (glowing){
              glowing = false;
              for (int i = 0; i < renderers.Length; i++){
                //Adding glow to main color
                renderers[i].materials[0].DisableKeyword("_EMISSION");


                //Removing glow from secondary color
                renderers[i].materials[1].EnableKeyword("_EMISSION");
              }
            }
          }
        }
    }
}
