using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public Vector3 flightAreaCenter = new Vector3(30, -24, 42);
    public Vector3 flightAreaSize = new Vector3(20, 5, 20);
    public float minWaitTime = 2f;
    public float maxWaitTime = 4f;

    private Vector3 targetPosition;
    private float waitTimer;

    void Start()
    {
        PickNewTarget();
    }

    void Update()
    {
        // Mover suavemente hacia la posición objetivo
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        transform.LookAt(targetPosition);

        // Si está cerca del destino, espera un poco y luego elige nuevo destino
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                PickNewTarget();
            }
        }
    }

    void PickNewTarget()
    {
        Vector3 halfSize = flightAreaSize * 0.5f;
        float x = Random.Range(-halfSize.x, halfSize.x);
        float y = Random.Range(-halfSize.y, halfSize.y);
        float z = Random.Range(-halfSize.z, halfSize.z);
        targetPosition = flightAreaCenter + new Vector3(x, y, z);

        waitTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(flightAreaCenter, flightAreaSize);
    }
}
