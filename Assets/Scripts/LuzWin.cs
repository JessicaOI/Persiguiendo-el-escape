using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzWin : MonoBehaviour
{
    public GameObject[] objetosActivables; // Lista de objetos que se pueden activar o desactivar
    public int objetosActivadosNecesarios = 3; // Número de objetos que deben estar activados
    public int objetosDesactivadosNecesarios = 2; // Número de objetos que deben estar desactivados

    private void Start()
    {
        // Desactivar todos los objetos al principio
        foreach (GameObject objeto in objetosActivables)
        {
            objeto.SetActive(false);
        }
    }

    public void ActivarDesactivarObjeto(int index)
    {
        GameObject objeto = objetosActivables[index];
        objeto.SetActive(!objeto.activeSelf); // Cambiar estado del objeto (activado/desactivado)
        VerificarCondiciones();
    }

    private void VerificarCondiciones()
    {
        int objetosActivados = 0;
        int objetosDesactivados = 0;

        // Contar objetos activados y desactivados
        foreach (GameObject objeto in objetosActivables)
        {
            if (objeto.activeSelf)
            {
                objetosActivados++;
            }
            else
            {
                objetosDesactivados++;
            }
        }

        // Verificar si se cumplen las condiciones
        if (objetosActivados == objetosActivadosNecesarios && objetosDesactivados == objetosDesactivadosNecesarios)
        {
            Debug.Log("Se han activado " + objetosActivadosNecesarios + " objetos y desactivado " + objetosDesactivadosNecesarios + " objetos.");
        }
    }
}
