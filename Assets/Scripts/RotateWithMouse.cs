using UnityEngine;

public class RotateWithMouse : MonoBehaviour
{
    public Camera cam;
    public float rotateSpeed = 5f;

    private GameObject selectedParentObject; // Referencia al objeto padre que será rotado
    private float rotatableTimer = 0f;
    private float rotatable2Timer = 0f;
    private bool hasWon = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Rotatable") || hit.transform.CompareTag("Rotatable2"))
                {
                    // Asignamos el padre del objeto seleccionado para rotar
                    selectedParentObject = hit.transform.parent != null ? hit.transform.parent.gameObject : null;
                }
            }
        }

        if (selectedParentObject != null && Input.GetMouseButton(0))
        {
            float rotateZ = Input.GetAxis("Mouse X") * rotateSpeed;
            selectedParentObject.transform.Rotate(0, 0, rotateZ, Space.World);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedParentObject = null; // Deselecciona el objeto padre cuando se suelta el botón del mouse
        }

        if (!hasWon)
        {
            CheckRotation();
        }
    }

    void CheckRotation()
    {
        CheckRotationForTag("Rotatable", 50, 60, ref rotatableTimer);
        CheckRotationForTag("Rotatable2", -60, -50, ref rotatable2Timer);

        if (rotatableTimer >= 1 && rotatable2Timer >= 1 && !hasWon)
        {
            Debug.Log("¡Has ganado!");
            hasWon = true;
            // Aquí puedes añadir cualquier otra lógica que necesites ejecutar al ganar.
        }
    }

    void CheckRotationForTag(string tag, float minAngle, float maxAngle, ref float timer)
    {
        bool isInCorrectRotation = false;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objectsWithTag)
        {
            // Asumimos que el padre es el que debe estar en el rango correcto de rotación.
            GameObject parentObj = obj.transform.parent != null ? obj.transform.parent.gameObject : obj;
            float zRotation = NormalizeAngle(parentObj.transform.eulerAngles.z);
            if (zRotation >= minAngle && zRotation <= maxAngle)
            {
                isInCorrectRotation = true;
            }
            else
            {
                isInCorrectRotation = false;
                break; // Si alguno de los objetos no está en el rango correcto, no necesitamos verificar más.
            }
        }

        if (isInCorrectRotation)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0; // Reinicia el temporizador si el objeto sale del rango.
        }
    }

    float NormalizeAngle(float angle)
    {
        angle = (angle > 180) ? angle - 360 : angle;
        return angle;
    }
}
