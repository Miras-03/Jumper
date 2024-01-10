using System.Collections;
using UnityEngine;
using UnityEngine.Pool; 

public sealed class SpawnManager : MonoBehaviour, IDeathObserver
{
    [SerializeField] private Obstacle prefab;
    private ObjectPool<Obstacle> pool;

    private Vector3 spawnPoint = new Vector3(30, 0, 0);
    private const int startSec = 2;
    private const int perSec = 4;

    private void Awake() => 
        pool = new ObjectPool<Obstacle>(CreateObject, GetObject, ReleaseObject, DestroyObject, 
            collectionCheck: false, 
            defaultCapacity: 3, 
            maxSize: 5);

    private void Start() => StartCoroutine(InstantiateWithDelay());

    public void ExecuteDeath() => StopAllCoroutines();

    private Obstacle CreateObject() => Instantiate(prefab, spawnPoint, prefab.transform.rotation, transform);

    private void GetObject(Obstacle obs) => obs.gameObject.SetActive(true);

    private void ReleaseObject(Obstacle obs) => obs.gameObject.SetActive(false);

    private void RemoveObstacle(Obstacle obs) => pool.Release(obs);

    private void DestroyObject(Obstacle obs) => Destroy(obs.gameObject);

    private IEnumerator InstantiateWithDelay()
    {
        yield return new WaitForSeconds(startSec);
        while (true)
        {
            var obs = pool.Get();
            ResetTransform(obs);
            obs.Init(RemoveObstacle);
            yield return new WaitForSeconds(perSec);
        }
    }

    private void ResetTransform(Obstacle obs)
    {
        obs.transform.position = spawnPoint;
        obs.transform.rotation = Quaternion.identity;
    }
}