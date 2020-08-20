using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBundlesPanel : Panel
{
    [SerializeField]
    private GameObject player;

    protected override void OnShow()
    {
        player.SetActive(false);
    }
}
