using Unity.VisualScripting;
using UnityEngine.EventSystems;

public abstract class VarUtils
{
    public static T? GetVar<T>(UIBehaviour source, string key) where T : struct
    {
        Variables variables = source.GetComponent<Variables>();
        if (variables == null)
        {
            return new T();
        }

        return variables.declarations.Get<T>(key);
    }
}