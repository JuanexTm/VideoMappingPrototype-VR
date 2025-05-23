using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ControladorNarrativa : MonoBehaviour
{
    public AudioSource audioNarracion;
    public Transform camaraPadre;              // tu empty con 5 cámaras
    public List<EventoVisual> eventos = new();

    private int indiceEvento = 0;

    void Start() => audioNarracion.Play();

    void Update()
    {
        if (indiceEvento >= eventos.Count) return;

        var ev = eventos[indiceEvento];
        if (audioNarracion.time >= ev.tiempoEvento)
        {
            StartCoroutine(EjecutarEvento(ev));
            indiceEvento++;
        }
    }

    /* ────────── EVENTO ────────── */
    IEnumerator EjecutarEvento(EventoVisual ev)
    {
        /* Fog */
        if (ev.cambiarFog)
            StartCoroutine(LerpFog(RenderSettings.fogDensity, ev.densidadFogObjetivo, ev.duracionFog));

        /* Movimiento */
        if (ev.moverCamara)
            StartCoroutine(LerpPosition(camaraPadre, camaraPadre.position, ev.nuevaPosicionCamara, ev.duracionMovimientoCamara));

        /* Rotación */
        if (ev.rotarCamara)
        {
            Quaternion rotInicial = camaraPadre.rotation;
            Quaternion rotFinal = Quaternion.Euler(ev.rotacionObjetivoEuler);
            StartCoroutine(LerpRotation(camaraPadre, rotInicial, rotFinal, ev.duracionRotacionCamara));
        }

        /* Activar / Desactivar */
        ev.objetosActivar.ForEach(o => { if (o) o.SetActive(true); });
        ev.objetosDesactivar.ForEach(o => { if (o) o.SetActive(false); });

        /* Escalas */
        ev.objetosEscalaIn.ForEach(o => { if (o) StartCoroutine(AnimarEscala(o, Vector3.zero, Vector3.one, 1f)); });
        ev.objetosEscalaOut.ForEach(o => { if (o) StartCoroutine(AnimarEscala(o, o.transform.localScale, Vector3.zero, 1f)); });

        yield break;
    }

    /* ────────── CORUTINAS DE INTERPOLACIÓN ────────── */

    IEnumerator LerpFog(float inicio, float fin, float dur)
    {
        float t = 0f;
        while (t < dur)
        {
            t += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(inicio, fin, t / dur);
            yield return null;
        }
        RenderSettings.fogDensity = fin;
    }

    IEnumerator LerpPosition(Transform tr, Vector3 ini, Vector3 fin, float dur)
    {
        float t = 0f;
        while (t < dur)
        {
            t += Time.deltaTime;
            tr.position = Vector3.Lerp(ini, fin, t / dur);
            yield return null;
        }
        tr.position = fin;
    }

    IEnumerator LerpRotation(Transform tr, Quaternion ini, Quaternion fin, float dur)
    {
        float t = 0f;
        while (t < dur)
        {
            t += Time.deltaTime;
            tr.rotation = Quaternion.Slerp(ini, fin, t / dur);
            yield return null;
        }
        tr.rotation = fin;
    }

    IEnumerator AnimarEscala(GameObject obj, Vector3 ini, Vector3 fin, float dur)
    {
        float t = 0f;
        while (t < dur)
        {
            t += Time.deltaTime;
            obj.transform.localScale = Vector3.Lerp(ini, fin, t / dur);
            yield return null;
        }
        obj.transform.localScale = fin;
        if (fin == Vector3.zero) obj.SetActive(false);
    }
}
