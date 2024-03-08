using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light myLight; // Luz del foco

    void Start()
    {
        myLight.enabled = false; // Asegúrate de que la luz del foco esté apagada al inicio
    }
}
