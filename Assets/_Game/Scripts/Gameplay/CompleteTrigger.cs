using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CompleteTrigger : Singleton<CompleteTrigger>
{
    public delegate void CompleteEvent();
    public event CompleteEvent OnComplete;

    public void Complete()
    {
        if (SaveManager.Instance)
        {
            TimeSpan time = TimeSpan.FromSeconds(SaveManager.Instance.GetTimePlayed() + MovementController.Instance.TimePlayed);
            if (SaveManager.Instance.CurrentMap == SaveManager.CampaignMapName)
            {
                AchievementManager.CompleteAchievement("complete_game");

                if (time.TotalMinutes <= 40)
                    AchievementManager.CompleteAchievement("complete_under_forty");
                else if (time.TotalMinutes >= 180)
                    AchievementManager.CompleteAchievement("complete_over_3_hours");
            }

            OnComplete?.Invoke();
        }

        Debug.Log("COMPLETED!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Complete();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Leg"))
            Complete();
    }
}
