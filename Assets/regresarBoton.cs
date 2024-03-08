using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Asegúrate de incluir este espacio de nombres

public class regresarBoton : MonoBehaviour
{
    public TextMeshProUGUI loadingText; // Referencia al texto de TextMeshPro

    public void regresar()
    {
        StartCoroutine(LoadSceneAsync("escena1"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Mientras la escena no esté completamente cargada
        while (!asyncLoad.isDone)
        {
            // Calcula el porcentaje de carga como un valor entre 0 y 1 y lo multiplica por 100 para obtener un porcentaje
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f) * 100;
            // Actualiza el texto de TextMeshPro para mostrar el porcentaje de carga
            loadingText.text = "Cargando... " + progress.ToString("F0") + "%";

            yield return null;
        }
    }
}
