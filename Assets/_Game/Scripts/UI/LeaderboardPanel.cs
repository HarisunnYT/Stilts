﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardPanel : Panel
{
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
