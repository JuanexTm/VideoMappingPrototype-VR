using UnityEngine;
using System.Collections;

public class ScaleInOut : MonoBehaviour
{
    public float scaleDuration = 1f;
    public float visibleDuration = 3f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero; // Start invisible

        StartCoroutine(ScaleSequence());
    }

    IEnumerator ScaleSequence()
    {
        // Aparecer (scale in)
        yield return StartCoroutine(ScaleOverTime(Vector3.zero, originalScale));

        // Tiempo visible
        yield return new WaitForSeconds(visibleDuration);

        // Desaparecer (scale out)
        yield return StartCoroutine(ScaleOverTime(originalScale, Vector3.zero));
    }

    IEnumerator ScaleOverTime(Vector3 start, Vector3 end)
    {
        float elapsed = 0f;
        while (elapsed < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(start, end, elapsed / scaleDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = end; // Asegura la escala final exacta
    }
}
