using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ControladorNarrativa : MonoBehaviour
{
    public AudioSource audioNarracion;
    public GameObject camaraPadre; // GameObject contenedor de las 5 cámaras
    public List<EventoVisual> eventos = new();

    private int eventoActual = 0;

    void Start()
    {
        audioNarracion.Play();
    }

    void Update()
    {
        if (eventoActual >= eventos.Count) return;

        float tiempoActual = audioNarracion.time;
        EventoVisual ev = eventos[eventoActual];

        if (tiempoActual >= ev.tiempoEvento)
        {
            EjecutarEvento(ev);
            eventoActual++;
        }
    }

    void EjecutarEvento(EventoVisual ev)
    {
        // Movimiento cámara
        if (ev.nuevaPosicionCamara.HasValue)
            camaraPadre.transform.position = ev.nuevaPosicionCamara.Value;

        if (ev.rotacionAdicionalCamara.HasValue)
            camaraPadre.transform.Rotate(ev.rotacionAdicionalCamara.Value);

        // Fog
        if (ev.fogDensity.HasValue)
            RenderSettings.fogDensity = ev.fogDensity.Value;

        // Activación y desactivación
        foreach (GameObject obj in ev.objetosActivar)
            if (obj != null) obj.SetActive(true);

        foreach (GameObject obj in ev.objetosDesactivar)
            if (obj != null) obj.SetActive(false);

        // Escala IN
        foreach (GameObject obj in ev.objetosEscalaIn)
            if (obj != null) StartCoroutine(AnimarEscala(obj, Vector3.zero, Vector3.one, 1f));

        // Escala OUT
        foreach (GameObject obj in ev.objetosEscalaOut)
            if (obj != null) StartCoroutine(AnimarEscala(obj, obj.transform.localScale, Vector3.zero, 1f));
    }

    IEnumerator AnimarEscala(GameObject obj, Vector3 inicio, Vector3 fin, float duracion)
    {
        float t = 0f;
        obj.transform.localScale = inicio;

        while (t < duracion)
        {
            t += Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(inicio, fin, t / duracion);
            yield return null;
        }

        obj.transform.localScale = fin;
        if (fin == Vector3.zero) obj.SetActive(false); // ocultar si desaparece
    }
}
