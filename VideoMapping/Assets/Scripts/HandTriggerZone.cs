using UnityEngine;

public class HandTriggerZone : MonoBehaviour
{
    public bool handInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            handInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            handInside = false;
        }
    }
}
