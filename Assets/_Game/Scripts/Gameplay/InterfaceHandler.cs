using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetable
{
    void Register();
    void OnReset();
}

public class InterfaceHandler
{
    private static List<IResetable> resetables = new List<IResetable>();

    public static void Register(IResetable resetable)
    {
        resetables.Add(resetable);
    }

    public static void OnReset()
    {
        foreach(var resetable in resetables)
            resetable.OnReset();
    }
}
