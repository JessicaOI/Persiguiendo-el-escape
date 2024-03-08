using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class regresarBoton : MonoBehaviour
{
    public void regresar()
    {
        SceneManager.LoadScene("escena1");
    }
}
