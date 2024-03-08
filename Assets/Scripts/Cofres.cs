using UnityEngine;

public class Cofre : MonoBehaviour
{
    public GameObject objetoReemplazo; // Objeto que reemplazará al cofre
    public Vector3 nuevasCoordenadas; // Coordenadas donde aparecerá el nuevo objeto
    public Quaternion nuevaRotacion; // Rotación del nuevo objeto

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Llave"))
        {
            // Destruir la llave
            Destroy(other.gameObject);

            // Instanciar el objeto de reemplazo en las coordenadas y rotación especificadas
            Instantiate(objetoReemplazo, nuevasCoordenadas, nuevaRotacion);

            // Destruir el cofre actual
            Destroy(gameObject);
        }
    }
}
