using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateWithMouse : MonoBehaviour
{
    public Camera cam;
    public float rotateSpeed = 5f;

    private GameObject selectedObject;
    private bool rotatableLock = false;
    private bool rotatable2Lock = false;

    private float rotatableTimer = 0f;
    private float rotatable2Timer = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if ((hit.transform.CompareTag("Rotatable") && !rotatableLock) ||
                    (hit.transform.CompareTag("Rotatable2") && !rotatable2Lock))
                {
                    selectedObject = hit.transform.parent != null ? hit.transform.parent.gameObject : hit.transform.gameObject;
                }
            }
        }

        if (selectedObject != null && Input.GetMouseButton(0))
        {
            float rotateZ = Input.GetAxis("Mouse X") * rotateSpeed;
            selectedObject.transform.Rotate(0, 0, rotateZ, Space.World);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedObject = null;
        }

        CheckRotation();
    }

    void CheckRotation()
    {
        GameObject[] rotatables = GameObject.FindGameObjectsWithTag("Rotatable");
        foreach (var obj in rotatables)
        {
            if (!rotatableLock)
            {
                float zRotation = NormalizeAngle(obj.transform.eulerAngles.z);
                if (zRotation >= 50 && zRotation <= 60)
                {
                    rotatableTimer += Time.deltaTime;
                    if (rotatableTimer >= 1)
                    {
                        Debug.Log("Rotatable en posición.");
                        rotatableLock = true;
                        break; // Si uno cumple la condición, sal del bucle.
                    }
                }
                else
                {
                    rotatableTimer = 0; // Reset si sale del rango.
                }
            }
        }

        GameObject[] rotatable2s = GameObject.FindGameObjectsWithTag("Rotatable2");
        foreach (var obj in rotatable2s)
        {
            if (!rotatable2Lock)
            {
                float zRotation = NormalizeAngle(obj.transform.eulerAngles.z);
                if (zRotation <= 95 && zRotation >= 85)
                {
                    rotatable2Timer += Time.deltaTime;
                    if (rotatable2Timer >= 1)
                    {
                        Debug.Log("Rotatable2 en posición.");
                        rotatable2Lock = true;
                        break; // Si uno cumple la condición, sal del bucle.
                    }
                }
                else
                {
                    rotatable2Timer = 0; // Reset si sale del rango.
                }
            }
        }

        if (rotatableLock && rotatable2Lock)
        {
            Debug.Log("¡Has ganado!");
            SceneManager.LoadScene("Final");
        }
    }

    float NormalizeAngle(float angle)
    {
        angle = (angle > 180) ? angle - 360 : angle;
        return angle;
    }
}
