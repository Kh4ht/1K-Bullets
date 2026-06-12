using System.Collections.Generic;
using KH;

public interface IKHIUnityMethods
{
    void IOnEnable() { }
    void IOnDisable() { }
    void IAwake() { }
    void IStart() { }
    void IUpdate() { }
    void IFixedUpdate() { }
}

public static class KHHelper
{
    public static void OnEnableAll(this List<IKHIUnityMethods> systems)
    {
        systems.KHForEach(p => p.IOnEnable());
    }

    public static void OnDisableAll(this List<IKHIUnityMethods> systems)
    {
        systems.KHForEach(p => p.IOnDisable());
    }

    public static void AwakeAll(this List<IKHIUnityMethods> systems)
    {
        systems.KHForEach(p => p.IAwake());
    }

    public static void StartAll(this List<IKHIUnityMethods> systems)
    {
        systems.KHForEach(p => p.IStart());
    }

    public static void UpdateAll(this List<IKHIUnityMethods> systems)
    {
        systems.KHForEach(p => p.IUpdate());
    }

    public static void FixedUpdateAll(this List<IKHIUnityMethods> systems)
    {
        systems.KHForEach(p => p.IFixedUpdate());
    }
}