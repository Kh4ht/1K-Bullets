#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[InitializeOnLoad]
public static class KHUIEditorHelper
{
    static KHUIEditorHelper()
    {
        ObjectFactory.componentWasAdded += OnComponentAdded;
    }

    private static void OnComponentAdded(Component component)
    {
        if (component is KH.KHUI)
        {
            while (ComponentUtility.MoveComponentUp(component))
            {
            }
        }

        if (component is SliderController)
        {
            while (ComponentUtility.MoveComponentUp(component))
            {
            }
        }
    }
}
#endif