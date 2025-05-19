using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorBoton : MonoBehaviour
{
    // Método público que puedes llamar desde el botón
    public void ReiniciarEscena()
    {
        SceneManager.LoadScene("SampleScene"); // Carga la escena 1 (o reemplaza con el nombre exacto)
    }
}
