using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Util 
{
    public static string ConvertFloatToTime(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        StringBuilder str = new StringBuilder();

        if (timeSpan.Hours > 0)
        {
            str.Append(timeSpan.Hours);
            str.Append("h ");
        }

        if (timeSpan.Minutes > 0)
        {
            str.Append(timeSpan.Minutes);
            str.Append("m ");
        }

        str.Append(timeSpan.Seconds);
        str.Append("s");

        return str.ToString();
    }
}
