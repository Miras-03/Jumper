using System.Collections;
using UnityEngine;

public sealed class BackgroundRepeat : MonoBehaviour, IDeathObserver
{
    private Coroutine coroutine;
    private Vector3 startPos;
    private float halfWidthOfBG;

    private void Awake()
    {
        startPos = transform.position;
        halfWidthOfBG = GetComponent<BoxCollider>().size.x / 2;
    }

    private void Start() => coroutine = StartCoroutine(CheckOrRepeat());

    private IEnumerator CheckOrRepeat()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            float currentXPos = transform.position.x;
            float withOfBG = startPos.x - halfWidthOfBG;
            if (currentXPos <= withOfBG)
                transform.position = startPos;
        }
    }

    public void ExecuteDeath()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}