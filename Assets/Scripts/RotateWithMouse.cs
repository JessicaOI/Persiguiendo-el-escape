using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    public Camera cam; // Referencia a tu cámara
    public float rotateSpeed = 5f; // Velocidad de rotación

    private GameObject selectedObject; // Objeto seleccionado para rotar

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Al hacer clic con el botón izquierdo del mouse
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Verifica si el objeto al que hiciste clic es el que quieres rotar
                if (hit.transform.CompareTag("Rotatable")) // Asegúrate de que tu objeto tenga este tag
                {
                    selectedObject = hit.transform.gameObject;
                }
            }
        }

        if (selectedObject != null && Input.GetMouseButton(0))
        {
            float rotateZ = Input.GetAxis("Mouse X") * rotateSpeed;

            // Rota el objeto seleccionado en el eje Z basado en elmovimiento horizontal del mouse
        selectedObject.transform.Rotate(0, 0, rotateZ, Space.World);
                }
        if (Input.GetMouseButtonUp(0)) // Al soltar el botón del mouse
        {
            // Deselecciona el objeto
            selectedObject = null;
        }
    
    if (Input.GetMouseButtonUp(0)) // Al soltar el botón del mouse
    {
        // Deselecciona el objeto
        selectedObject = null;
    }
}
}