using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonView : MonoBehaviour
{

    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    private Vector3 velocity;
    private Rigidbody rb;

    public GameObject eyes; // Asegúrate de que este es tu objeto de cámara

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // Opcional: Bloquea el cursor al centro de la pantalla
    }

    void Update()
    {
        float moveFB = Input.GetAxis("Vertical") * speed;
        float moveLR = Input.GetAxis("Horizontal") * speed;

        Vector3 movement = transform.right * moveLR + transform.forward * moveFB;
        velocity = movement;

        // Rotación de la cámara en el eje Y
        float rotX = Input.GetAxis("Mouse X") * sensitivity;
        float rotY = -Input.GetAxis("Mouse Y") * sensitivity; // Asegúrate de invertir el signo si es necesario
        eyes.transform.Rotate(new Vector3(rotY, 0f, 0f));
        transform.Rotate(new Vector3(0f, rotX, 0f));
    }

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}

