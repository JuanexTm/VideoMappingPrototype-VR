using UnityEngine;
using System.Collections.Generic;

public class TimelineVisual : MonoBehaviour
{
    public List<EventoVisual> eventos;
    public AudioSource narracion;
    private HashSet<float> eventosActivados = new HashSet<float>();

    void Update()
    {
        float tiempo = narracion.time;

        foreach (var evento in eventos)
        {
            if (tiempo >= evento.tiempoActivacion && !eventosActivados.Contains(evento.tiempoActivacion))
            {
                if (evento.objeto != null)
                    evento.objeto.SetActive(evento.activar);

                eventosActivados.Add(evento.tiempoActivacion);
            }
        }
    }
}
