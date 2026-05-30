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
    private static bool _isUpdating = false;
    private static bool _isFixedUpdating = false;

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
        _isUpdating = true;

        for (int i = _observers.Count - 1; i >= 0; i--)
        {
            if (i < _observers.Count)
            {
                _observers[i].OUpdate();
            }
        }

        _isUpdating = false;

        // Add pending observers after update is complete
        if (_pendingObservers.Count > 0)
        {
            _observers.AddRange(_pendingObservers);
            _pendingObservers.Clear();
        }
    }

    private void FixedUpdate()
    {
        _isFixedUpdating = true;

        for (int i = _observers.Count - 1; i >= 0; i--)
        {
            if (i < _observers.Count)
            {
                _observers[i].OFixedUpdate();
            }
        }

        _isFixedUpdating = false;

        // Add pending fixed observers after fixed update is complete
        if (_pendingFixedObservers.Count > 0)
        {
            _observers.AddRange(_pendingFixedObservers);
            _pendingFixedObservers.Clear();
        }
    }

    #endregion
    // █████████████████████████████████████████████████████████████████████████████████████████████████
    #region PUBLIC METHODS
    // █████████████████████████████████████████████████████████████████████████████████████████████████

    /// <summary>
    /// Register an observer to receive both Update and FixedUpdate calls
    /// </summary>
    public static void RegisterObserver(IUpdateObserver observer)
    {
        if (observer == null)
            return;

        if (_isUpdating || _isFixedUpdating)
        {
            _pendingObservers.Add(observer);
            _pendingFixedObservers.Add(observer);
        }
        else
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }
    }

    /// <summary>
    /// Register an observer for Update only
    /// </summary>
    public static void RegisterObserverUpdateOnly(IUpdateObserver observer)
    {
        if (observer == null) return;

        if (_isUpdating)
        {
            _pendingObservers.Add(observer);
        }
        else
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }
    }

    /// <summary>
    /// Register an observer for FixedUpdate only
    /// </summary>
    public static void RegisterObserverFixedUpdateOnly(IUpdateObserver observer)
    {
        if (observer == null)
            return;

        if (_isFixedUpdating)
        {
            _pendingFixedObservers.Add(observer);
        }
        else
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }
    }

    /// <summary>
    /// Unregister an observer from all updates
    /// </summary>
    public static void UnregisterObserver(IUpdateObserver observer)
    {
        if (observer == null)
            return;

        // Remove from main list
        if (!_isUpdating && !_isFixedUpdating)
        {
            _observers.Remove(observer);
        }

        // Remove from pending lists
        _pendingObservers.Remove(observer);
        _pendingFixedObservers.Remove(observer);
    }

    /// <summary>
    /// Clear all registered observers
    /// </summary>
    public static void ClearAllObservers()
    {
        if (!_isUpdating && !_isFixedUpdating)
        {
            _observers.Clear();
        }
        _pendingObservers.Clear();
        _pendingFixedObservers.Clear();
    }

    /// <summary>
    /// Check if an observer is registered
    /// </summary>
    public static bool IsObserverRegistered(IUpdateObserver observer)
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