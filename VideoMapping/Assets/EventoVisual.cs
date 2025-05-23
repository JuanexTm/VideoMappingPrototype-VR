using System;
using UnityEngine;

[Serializable]
public class EventoVisual
{
    public float tiempo; // en segundos
    public GameObject[] activarObjetos;
    public GameObject[] desactivarObjetos;
    public float? fogDensity = null; // usar null para ignorar
    public Vector3? nuevaPosicionCamara = null;
    public Vector3? rotacionAdicionalCamara = null;
    public string nombreEvento; // solo para debugging
}
