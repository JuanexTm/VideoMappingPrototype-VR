using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class EventoVisual
{
    public float tiempoEvento = 0f;

    [Header("Cámara")]
    public Vector3? nuevaPosicionCamara;
    public Vector3? rotacionAdicionalCamara;

    [Header("Fog")]
    public float? fogDensity;

    [Header("Activar / Desactivar Objetos")]
    public List<GameObject> objetosActivar = new();
    public List<GameObject> objetosDesactivar = new();

    [Header("Escala Animada (opcional)")]
    public List<GameObject> objetosEscalaIn = new();
    public List<GameObject> objetosEscalaOut = new();

    [Header("Notas (no afecta el código)")]
    [TextArea]
    public string descripcion;
}
