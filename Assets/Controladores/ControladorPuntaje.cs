using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorPuntaje : MonoBehaviour
{
    public TMP_Text textoPuntaje;
    private int puntaje = 0;

    public void SumarPunto()
    {
        puntaje++;
        textoPuntaje.text = "Puntaje: " + puntaje.ToString();
    }

    void Start()
    {
        textoPuntaje.text = "Puntaje: 0";
        //SumarPunto(); // debería mostrar: Puntaje: 1
    }

}
