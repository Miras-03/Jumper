using System;
using UnityEngine;

public sealed class Obstacle : MonoBehaviour
{
    private Action<Obstacle> OnRelease;

    private const float bound = -15;
    private const int startSec = 3;
    private const int interval = 1;

    private void Start() => InvokeRepeating(nameof(CheckOrDestroy), startSec, interval);

    public void Init(Action<Obstacle> OnRelease) => this.OnRelease = OnRelease;

    private void CheckOrDestroy()
    {
        float currentXPos = transform.position.x;
        if (currentXPos <= bound)
            OnRelease(this);
    }
}
