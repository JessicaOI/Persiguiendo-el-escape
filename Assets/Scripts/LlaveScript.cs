using UnityEngine;

public class LlaveScript : MonoBehaviour
{
    public GameObject llavePrefab; // El prefab de la llave que quieres instanciar
    public bool acertijoResuelto = false;
    public Vector3 posicionCreacion; // La posición donde quieres crear la llave

    void Start()
    {
        if (acertijoResuelto)
        {
            // Si el acertijo está resuelto, instanciar la llave en la posición deseada
            Instantiate(llavePrefab, posicionCreacion, Quaternion.identity);
        }
    }
}
