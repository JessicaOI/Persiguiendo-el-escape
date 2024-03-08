using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateWithMouse : MonoBehaviour
{
    public Camera cam;
    public float rotateSpeed = 5f;

    private GameObject selectedParentObject; // Referencia al objeto padre que ser� rotado
    private float rotatableTimer = 0f;
    private float rotatable2Timer = 0f;
    private float rotatable3Timer = 0f; // Nueva variable para el tercer objeto
    private bool hasWon = false;
    public GameObject victoryCanvas;
    public GameObject normalCanvas;

    void Start()
    {
        // Mostrar y desbloquear el cursor al pausar el juego
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Rotatable") || hit.transform.CompareTag("Rotatable2") || hit.transform.CompareTag("Rotatable3"))
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
            selectedParentObject = null; // Deselecciona el objeto padre cuando se suelta el bot�n del mouse
        }

        if (!hasWon)
        {
            CheckRotation();
        }
    }


    void CheckRotation()
    {
        CheckRotationForTag("Rotatable", 51, 65, ref rotatableTimer);
        CheckRotationForTag("Rotatable2", -64, -51, ref rotatable2Timer);
        CheckRotationForTag("Rotatable3", 170, 180, ref rotatable3Timer); // Usa la nueva variable aqu�

        if (rotatableTimer >= 1 && rotatable2Timer >= 1 && rotatable3Timer >= 1 && !hasWon) // Modifica esta condici�n
        {
            Debug.Log("�Has ganado!");
            hasWon = true;
            if (victoryCanvas != null)
            {
                normalCanvas.SetActive(false);
                victoryCanvas.SetActive(true); // Habilita el Canvas
                
            }
            else
            {
                Debug.LogWarning("No se ha asignado un Victory Canvas al script.");
            }
        }
    }


    void CheckRotationForTag(string tag, float minAngle, float maxAngle, ref float timer)
    {
        bool isInCorrectRotation = false;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objectsWithTag)
        {
            // Asumimos que el padre es el que debe estar en el rango correcto de rotaci�n.
            GameObject parentObj = obj.transform.parent != null ? obj.transform.parent.gameObject : obj;
            float zRotation = NormalizeAngle(parentObj.transform.eulerAngles.z);
            if (zRotation >= minAngle && zRotation <= maxAngle)
            {
                isInCorrectRotation = true;
            }
            else
            {
                isInCorrectRotation = false;
                break; // Si alguno de los objetos no est� en el rango correcto, no necesitamos verificar m�s.
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
