using UnityEngine;

public sealed class DeathManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private BackgroundRepeat bgRepeat;
    [SerializeField] private MoveLeft[] moveLeft;
    private Death death;

    private void Awake() => death = Death.Instance;

    private void OnEnable()
    {
        death.Add(spawnManager);
        death.Add(bgRepeat);
        foreach (var move in moveLeft)
             death.Add(move);
    }

    private void OnDestroy() => death.Clear();
}