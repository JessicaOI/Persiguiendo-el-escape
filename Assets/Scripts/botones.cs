using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Aseg�rate de agregar este namespace

public class botones : MonoBehaviour
{
    public LlaveScript llaveScript;
    public TextMeshProUGUI textoUI;
    public TextMeshProUGUI mensajeEspecialUI; // Para mostrar mensajes como "Correcto"
    private string textoActual = "";

    void Start()
    {
        if (textoUI == null || mensajeEspecialUI == null)
        {
            Debug.LogError("Uno o m�s componentes no se han asignado en el inspector.");
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
            Debug.Log($"El bot�n del n�mero {numero} fue presionado.");

            if (textoActual.Length == 4)
            {
                if (textoActual == "7165")
                {
                    mensajeEspecialUI.text = "Correcto"; // Mostrar mensaje de correcto
                    //StartCoroutine(CambioDeEscena());  
                    SceneManager.LoadScene("escena1"); // Cambiar a la escena "escena1"
                    llaveScript.acertijo1Resuelto = true;
                    llaveScript.MostrarLlave();
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
    

    // Otras funciones como Numero1(), Numero7(), etc., deber�an llamar a PresionarBoton() con el n�mero correspondiente
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
