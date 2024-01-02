using System.Collections;
using UnityEngine;

public sealed class SpawnManager : MonoBehaviour, IDeathObserver
{
    [SerializeField] private Transform prefab;
    private Coroutine coroutine;

    private Vector3 spawnPoint = new Vector3(30, 0, 0);
    private const int startSec = 2;
    private const int perSec = 4;

    private void Start() => coroutine = StartCoroutine(InstantiateWithDelay());

    private IEnumerator InstantiateWithDelay()
    {
        yield return new WaitForSeconds(startSec);
        while (true)
        {
            Instantiate();
            yield return new WaitForSeconds(perSec);
        }
    }

    private void Instantiate() => Instantiate(prefab, spawnPoint, prefab.rotation);

    public void ExecuteDeath()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}