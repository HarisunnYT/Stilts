using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamManager : PersistentSingleton<SteamManager>
{
    protected override void Initialize()
    {
        SteamClient.Init(1394510);
    }

    private void OnApplicationQuit()
    {
#if !UNITY_EDITOR
        SteamClient.Shutdown();
#endif
    }
}
