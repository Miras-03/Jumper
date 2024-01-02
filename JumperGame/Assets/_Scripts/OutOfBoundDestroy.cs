using UnityEngine;

public sealed class OutOfBoundDestroy : MonoBehaviour
{
    private const float bound = -15;

    private void FixedUpdate() => CheckOrDestroy();

    private void CheckOrDestroy()
    {
        float currentXPos = transform.position.x;
        if (currentXPos <= bound)
            Destroy(gameObject);
    }
}
