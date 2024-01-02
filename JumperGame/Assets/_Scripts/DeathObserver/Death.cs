using System.Collections.Generic;

public sealed class Death
{
    private static Death instance;

    public static Death Instance
    {
        get
        {
            if (instance == null)
                instance = new Death();
            return instance;
        }
    }

    private List<IDeathObserver> observers = new List<IDeathObserver>();

    public void Add(IDeathObserver observer) => observers.Add(observer);

    public void Clear() => observers.Clear();

    public void NotifyObservers()
    {
        foreach (IDeathObserver observer in observers)
            observer.ExecuteDeath();
    }
}
