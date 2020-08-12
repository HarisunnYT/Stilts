using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamManager : PersistentSingleton<SteamManager>
{
    private void Start()
    {
        SteamClient.Init(1394510);
    }
}
