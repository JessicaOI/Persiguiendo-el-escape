using UnityEngine;

public class LuzWin : MonoBehaviour
{
    public GameObject objetoControlado;
    private bool encendido = true;

    public void CambiarEstado()
    {
        encendido = !encendido;
        objetoControlado.SetActive(encendido);
        
        if (encendido)
        {
            Debug.Log(objetoControlado.name + " está encendido.");
        }
        else
        {
            Debug.Log(objetoControlado.name + " está apagado.");
        }
    }
}
