using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour
{
    public float fadeDuration = 4f;
    public float visibleDuration = 40f;

    private Material material;
    private Color baseColor;

    void Start()
    {
        // Crear una instancia del material para que no afecte otros objetos
        material = GetComponent<Renderer>().material;
        baseColor = material.color;

        // Forzar el alpha a 0 al principio
        Color transparentColor = baseColor;
        transparentColor.a = 0f;
        material.color = transparentColor;

        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        yield return StartCoroutine(Fade(0f, 3f)); // Fade In
        yield return new WaitForSeconds(visibleDuration); // Visible
        yield return StartCoroutine(Fade(3f, 0f)); // Fade Out
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            Color newColor = baseColor;
            newColor.a = alpha;
            material.color = newColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Asegura el valor final
        Color finalColor = baseColor;
        finalColor.a = to;
        material.color = finalColor;
    }
}
