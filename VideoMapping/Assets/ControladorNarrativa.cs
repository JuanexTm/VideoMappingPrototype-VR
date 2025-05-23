using UnityEngine;
using System.Collections.Generic;

public class ControladorNarrativa : MonoBehaviour
{
    public AudioSource audioNarracion;
    public Camera camaraPrincipal;
    public List<EventoVisual> eventos;
    private int eventoActual = 0;
    private float tiempoPrevio = 0f;

    void Start()
    {
        audioNarracion.Play();
    }

    void Update()
    {
        if (eventoActual >= eventos.Count) return;

        float tiempoActual = audioNarracion.time;
        EventoVisual evento = eventos[eventoActual];

        if (tiempoActual >= evento.tiempo)
        {
            EjecutarEvento(evento);
            eventoActual++;
        }

        tiempoPrevio = tiempoActual;
    }

    void EjecutarEvento(EventoVisual ev)
    {
        if (!string.IsNullOrEmpty(ev.nombreEvento))
            Debug.Log("Ejecutando: " + ev.nombreEvento);

        foreach (GameObject obj in ev.activarObjetos) obj.SetActive(true);
        foreach (GameObject obj in ev.desactivarObjetos) obj.SetActive(false);

        if (ev.fogDensity.HasValue)
            RenderSettings.fogDensity = ev.fogDensity.Value;

        if (ev.nuevaPosicionCamara.HasValue)
            camaraPrincipal.transform.position = ev.nuevaPosicionCamara.Value;

        if (ev.rotacionAdicionalCamara.HasValue)
            camaraPrincipal.transform.Rotate(ev.rotacionAdicionalCamara.Value);
    }
}
