using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region FIELDS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private static readonly List<IUpdateObserver> _observers = new();
    private static readonly List<IUpdateObserver> _pendingObservers = new();
    private static readonly List<IUpdateObserver> _pendingFixedObservers = new();

    private static bool _isUpdating;
    private static bool _isFixedUpdating;

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region UNITY EVENTS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private void Awake()
    {
        // Make this persistent across scenes
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        CleanupObservers();

        _isUpdating = true;

        for (int i = _observers.Count - 1; i >= 0; i--)
        {
            var observer = _observers[i];

            if (IsInvalid(observer))
                continue;

            observer.OUpdate();
        }

        _isUpdating = false;

        FlushPending();
    }

    private void FixedUpdate()
    {
        CleanupObservers();

        _isFixedUpdating = true;

        for (int i = _observers.Count - 1; i >= 0; i--)
        {
            var observer = _observers[i];

            if (IsInvalid(observer))
                continue;

            observer.OFixedUpdate();
        }

        _isFixedUpdating = false;

        FlushPending();
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PRIVATE
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    private static void CleanupObservers()
    {
        _observers.RemoveAll(IsInvalid);
        _pendingObservers.RemoveAll(IsInvalid);
        _pendingFixedObservers.RemoveAll(IsInvalid);
    }

    private static bool IsInvalid(IUpdateObserver observer)
    {
        if (observer == null)
            return true;

        if (observer is UnityEngine.Object unityObj && unityObj == null)
            return true;

        return false;
    }

    private static void FlushPending()
    {
        if (_pendingObservers.Count > 0)
        {
            for (int i = 0; i < _pendingObservers.Count; i++)
            {
                var obs = _pendingObservers[i];
                if (!IsInvalid(obs) && !_observers.Contains(obs))
                    _observers.Add(obs);
            }
            _pendingObservers.Clear();
        }

        if (_pendingFixedObservers.Count > 0)
        {
            for (int i = 0; i < _pendingFixedObservers.Count; i++)
            {
                var obs = _pendingFixedObservers[i];
                if (!IsInvalid(obs) && !_observers.Contains(obs))
                    _observers.Add(obs);
            }
            _pendingFixedObservers.Clear();
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region STATIC
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    public static void RegisterObserver(IUpdateObserver observer)
    {
        if (IsInvalid(observer))
            return;

        if (_isUpdating || _isFixedUpdating)
        {
            _pendingObservers.Add(observer);
            _pendingFixedObservers.Add(observer);
        }
        else
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }
    }

    public static void RegisterObserverUpdateOnly(IUpdateObserver observer)
    {
        if (IsInvalid(observer))
            return;

        if (_isUpdating)
        {
            _pendingObservers.Add(observer);
        }
        else if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public static void RegisterObserverFixedOnly(IUpdateObserver observer)
    {
        if (IsInvalid(observer))
            return;

        if (_isFixedUpdating)
        {
            _pendingFixedObservers.Add(observer);
        }
        else if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public static void UnregisterObserver(IUpdateObserver observer)
    {
        if (IsInvalid(observer))
            return;

        _observers.Remove(observer);
        _pendingObservers.Remove(observer);
        _pendingFixedObservers.Remove(observer);
    }

    public static void ClearAllObservers()
    {
        _observers.Clear();
        _pendingObservers.Clear();
        _pendingFixedObservers.Clear();
    }

    public static bool IsRegistered(IUpdateObserver observer)
    {
        return _observers.Contains(observer) ||
               _pendingObservers.Contains(observer) ||
               _pendingFixedObservers.Contains(observer);
    }

    #endregion
}

// █████████████████████████████████████████████████████████████████████████████████████████████████
#region INTERFACE
// █████████████████████████████████████████████████████████████████████████████████████████████████

public interface IUpdateObserver
{
    void OUpdate();
    void OFixedUpdate();
}

#endregion