using UnityEngine;
using System.Collections;

public class SecuenciaPisadas : MonoBehaviour
{
    public float intervalo = 1f; // tiempo entre cada pisada
    private Transform[] pisadas;

    void OnEnable()
    {
        // Obtener hijos y ocultarlos
        pisadas = GetComponentsInChildren<Transform>(true);
        foreach (var p in pisadas)
        {
            if (p != transform) p.gameObject.SetActive(false);
        }

        // Iniciar secuencia
        StartCoroutine(MostrarPisadasSecuencialmente());
    }

    IEnumerator MostrarPisadasSecuencialmente()
    {
        foreach (var p in pisadas)
        {
            if (p != transform)
            {
                p.gameObject.SetActive(true);
                yield return new WaitForSeconds(intervalo);
            }
        }
    }
}
