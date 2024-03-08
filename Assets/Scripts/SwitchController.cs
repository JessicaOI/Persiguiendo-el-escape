using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject luzObjeto;
    public GameObject[] pointLights;
    public bool luz;
    private bool luzOnOff;

    public LlaveScript llaveScript;

    public void OnOffLuz()
    {
        luzOnOff = !luzOnOff;
        if (luzOnOff == true)
        {
            luzObjeto.SetActive(true);
            Debug.Log(luzObjeto.name + " habilitado.");
        }
        else
        {
            luzObjeto.SetActive(false);
            Debug.Log(luzObjeto.name + " deshabilitado.");
        }

        // Verificar si se cumplen las condiciones de victoria
        bool ganaste = true;
        foreach (GameObject pointLight in pointLights)
        {
            if ((pointLight.name == "Point Light (2)") ||
                (pointLight.name == "Point Light (5)") ||
                (pointLight.name == "Point Light (6)"))
            {
                if (!pointLight.activeSelf)
                {
                    ganaste = false;
                    break;
                }
            }
            else if ((pointLight.name == "Point Light (3)") ||
                     (pointLight.name == "Point Light (4)"))
            {
                if (pointLight.activeSelf)
                {
                    ganaste = false;
                    break;
                }
            }
        }

        if (ganaste)
        {
            Debug.Log("¡Ganaste!");
            llaveScript.acertijoResuelto = true;
        }
    }
}
