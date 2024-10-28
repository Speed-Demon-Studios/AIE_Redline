using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamAchievementManager : MonoBehaviour
{

    public void GetAchievement(string AchievementID)
    {
        //bool gotAchievement;
        //
        //Steamworks.SteamUserStats.GetAchievement(AchievementID, out gotAchievement);
        //
        //if (gotAchievement == false)
        //{
        //    Steamworks.SteamUserStats.SetAchievement(AchievementID);
        //    Steamworks.SteamUserStats.StoreStats();
        //}

        return;
    }

    public void CheckAllAchievementProgress(int p1Placement)
    {
        //RacerDetails rDeets = GameManager.gManager.players[0].GetComponent<RacerDetails>();
        //bool gotAchievement;
        //
        //if (p1Placement > 0)
        //{
        //    // Checking for WIN related achievements
        //    if (p1Placement == 1)
        //    {
        //        int currentWins;
        //        Steamworks.SteamUserStats.GetStat("RaceWins", out currentWins);
        //        currentWins++;
        //        Steamworks.SteamUserStats.SetStat("RaceWins", currentWins);
        //
        //        // "New Rider" - Win 1 race.
        //        Steamworks.SteamUserStats.GetAchievement("NewRider", out gotAchievement);
        //        if (currentWins == 1)
        //        {
        //            Steamworks.SteamUserStats.SetAchievement("NewRider");
        //        }
        //        Steamworks.SteamUserStats.StoreStats();
        //
        //        // "Redline Cadet" - Win 5 races.
        //        Steamworks.SteamUserStats.GetAchievement("RedlineCadet", out gotAchievement);
        //        if (currentWins == 5)
        //        {
        //            Steamworks.SteamUserStats.SetAchievement("RedlineCadet");
        //        }
        //        Steamworks.SteamUserStats.StoreStats();
        //    }
        //}

        return;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
