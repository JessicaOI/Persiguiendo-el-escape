using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finalMenu : MonoBehaviour
{
    // Función para reiniciar la escena
    public void Reiniciar()
    {
        // Carga la escena actualmente activa (reinicia)
        SceneManager.LoadScene("escena1");
    }

    // Función para salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
