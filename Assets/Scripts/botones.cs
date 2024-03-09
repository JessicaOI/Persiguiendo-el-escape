using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Asegúrate de agregar este namespace

public class botones : MonoBehaviour
{
    public TextMeshProUGUI textoUI;
    public TextMeshProUGUI mensajeEspecialUI; // Para mostrar mensajes como "Correcto"
    private string textoActual = "";

    void Start()
    {
        if (textoUI == null || mensajeEspecialUI == null)
        {
            Debug.LogError("Uno o más componentes no se han asignado en el inspector.");
        }
        // Mostrar y desbloquear el cursor al pausar el juego
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PresionarBoton(string numero)
    {
        if (textoActual.Length <= 4)
        {
            textoActual += numero;
            textoUI.text = textoActual;
            Debug.Log($"El botón del número {numero} fue presionado.");

            if (textoActual.Length == 4)
            {
                if (textoActual == "7165")
                {
                    mensajeEspecialUI.text = "Correcto"; // Mostrar mensaje de correcto
                    GlobalVariables.llaveTele += 1;
                    //StartCoroutine(CambioDeEscena());
                    SceneManager.LoadScene("escena1"); // Cambiar a la escena "escena1"
                }
                else
                {
                    textoActual = "";
                    textoUI.text = textoActual;
                }
            }
        }
    }

    /*IEnumerator CambioDeEscena()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("escena1"); // Cambiar a la escena "escena1"
    }*/
    

    // Otras funciones como Numero1(), Numero7(), etc., deberían llamar a PresionarBoton() con el número correspondiente
    public void Numero1() { PresionarBoton("1"); }
    public void Numero7() { PresionarBoton("7"); }
    public void Numero6() { PresionarBoton("6"); }
    public void Numero5() { PresionarBoton("5"); }
    public void Numero2() { PresionarBoton("2"); }
    public void Numero3() { PresionarBoton("3"); }
    public void Numero4() { PresionarBoton("4"); }
    public void Numero8() { PresionarBoton("8"); }
    public void Numero9() { PresionarBoton("9"); }
    public void Numero0() { PresionarBoton("0"); }
}
