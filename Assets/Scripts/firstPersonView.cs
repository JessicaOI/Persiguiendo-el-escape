using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Aseg�rate de incluir el namespace para TextMeshPro
using TMPro;
using UnityEngine.SceneManagement;

public class FirstPersonView : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    private Vector3 velocity;
    private Rigidbody rb;

    public GameObject eyes; // Tu objeto de c�mara
    private float rotY = 0f;
    public float minY = -60f;
    public float maxY = 60f;

    public float reach = 5f; // Distancia a la que el jugador puede agarrar objetos
    private GameObject objectInHand; // Objeto que el jugador est� sujetando actualmente
    public LayerMask interactableLayer; // Capa en la que se encuentran los objetos interactuables
    public LayerMask interactableLayerLuz;
    // Cambia el tipo de 'label' a TMP_Text para trabajar con TextMeshPro
    public TMP_Text label;

    public int rango; // Agrega esta variable para el rango del rayo
    public Camera camara; // Agrega esta variable para la cámara

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        if (label != null)
        {
            label.text = ""; // Inicializar el texto vac�o
        }
    }

    void Update()
    {
        MovePlayer();
        LookAround();

        Ray ray = new Ray(eyes.transform.position, eyes.transform.forward);
        RaycastHit hit;

        

        // Primero, verificamos si el jugador est� sosteniendo un objeto.
        if (objectInHand != null)
        {
            // El jugador ya est� sosteniendo un objeto, asegurarse de que el texto est� vac�o.
            if (label != null) label.text = "";

            // Verificar si el jugador decide soltar el objeto.
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseObject();
            }
        }

        else if (Physics.Raycast(ray, out hit, reach, interactableLayerLuz))
        {
            if (hit.collider.GetComponent<SwitchController>() != null)
            {
                if (label != null) label.text = "Presiona E para Interactuar";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.GetComponent<SwitchController>().luz == true)
                    {
                        hit.collider.GetComponent<SwitchController>().OnOffLuz();
                    }
                }
            }
            else
            {
                if (label != null) label.text = "";
            }
        }

        else if (Physics.Raycast(ray, out hit, reach, interactableLayer)) // Luego, si no est� sosteniendo un objeto, buscamos uno para agarrar.
        {
            // Si el objeto tiene la etiqueta "television", cambiamos la interacci�n
            if (hit.collider.gameObject.CompareTag("television"))
            {
                if (label != null) label.text = "Presiona E"; // Cambiar mensaje para televisi�n

                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene("tv"); // Cargar la escena "tv"
                }
            }
            else // Otros objetos interactuables
            {
                if (label != null) label.text = "Presiona E";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GrabObject(hit.collider.gameObject);
                }
            }
        }
        else
        {
            // No hay objetos cerca o enfocables, asegurarse de que el texto est� vac�o.
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
        obj.GetComponent<Rigidbody>().isKinematic = true; // Hacer que el objeto no reaccione a f�sicas mientras es sostenido
        obj.transform.SetParent(eyes.transform); // Hacer que el objeto siga la c�mara
    }

    void ReleaseObject()
    {
        if (objectInHand != null)
        {
            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.transform.SetParent(null); // Remover el objeto de ser hijo de la c�mara
            objectInHand = null;
        }
    }
}
