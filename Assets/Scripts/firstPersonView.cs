using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Asegúrate de incluir el namespace para TextMeshPro
using TMPro;

public class FirstPersonView : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    private Vector3 velocity;
    private Rigidbody rb;

    public GameObject eyes; // Tu objeto de cámara
    private float rotY = 0f;
    public float minY = -60f;
    public float maxY = 60f;

    public float reach = 10f; // Distancia a la que el jugador puede agarrar objetos
    private GameObject objectInHand; // Objeto que el jugador está sujetando actualmente
    public LayerMask interactableLayer; // Capa en la que se encuentran los objetos interactuables

    // Cambia el tipo de 'label' a TMP_Text para trabajar con TextMeshPro
    public TMP_Text label;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        if (label != null)
        {
            label.text = ""; // Inicializar el texto vacío
        }
    }

    void Update()
    {
        MovePlayer();
        LookAround();

        Ray ray = new Ray(eyes.transform.position, eyes.transform.forward);
        RaycastHit hit;

        // Primero, verificamos si el jugador está sosteniendo un objeto.
        if (objectInHand != null)
        {
            // El jugador ya está sosteniendo un objeto, asegurarse de que el texto esté vacío.
            if (label != null) label.text = "";

            // Verificar si el jugador decide soltar el objeto.
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseObject();
            }
        }
        else if (Physics.Raycast(ray, out hit, reach, interactableLayer)) // Luego, si no está sosteniendo un objeto, buscamos uno para agarrar.
        {
            // Hay un objeto enfocado y disponible para ser agarrado.
            if (label != null) label.text = "Press E to grab";

            if (Input.GetKeyDown(KeyCode.E))
            {
                GrabObject(hit.collider.gameObject);
            }
        }
        else
        {
            // No hay objetos cerca o enfocables, asegurarse de que el texto esté vacío.
            if (label != null) label.text = "";
        }
    }



    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void MovePlayer()
    {
        if (MenuPausa.JuegoPausado) return;

        float moveFB = Input.GetAxis("Vertical") * speed;
        float moveLR = Input.GetAxis("Horizontal") * speed;

        Vector3 movement = transform.right * moveLR + transform.forward * moveFB;
        velocity = movement;
    }

    private void LookAround()
    {
        if (MenuPausa.JuegoPausado) return;

        float rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY += Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, minY, maxY);

        eyes.transform.localEulerAngles = new Vector3(-rotY, 0f, 0f);
        transform.Rotate(new Vector3(0f, rotX, 0f));
    }


    void GrabObject(GameObject obj)
    {
        objectInHand = obj;
        obj.GetComponent<Rigidbody>().isKinematic = true; // Hacer que el objeto no reaccione a físicas mientras es sostenido
        obj.transform.SetParent(eyes.transform); // Hacer que el objeto siga la cámara
    }

    void ReleaseObject()
    {
        if (objectInHand != null)
        {
            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.transform.SetParent(null); // Remover el objeto de ser hijo de la cámara
            objectInHand = null;
        }
    }
}
