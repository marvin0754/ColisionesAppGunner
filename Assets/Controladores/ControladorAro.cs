using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAro : MonoBehaviour
{
    public ControladorPuntaje controladorPuntaje; // Asignar en Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bala"))
        {
            controladorPuntaje.SumarPunto();
            Destroy(gameObject); // Desaparece el aro
        }
    }
}
