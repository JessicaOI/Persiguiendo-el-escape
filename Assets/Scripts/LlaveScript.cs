using UnityEngine;

public class LlaveScript : MonoBehaviour
{
    public bool acertijo1Resuelto = false;

    // Método para mostrar la llave si los acertijos están resueltos

    public void MostrarLlave()
    {
        if (acertijo1Resuelto == true)
        {
            // Activa el objeto de la llave en la escena
            gameObject.SetActive(true);
        }
    }
}