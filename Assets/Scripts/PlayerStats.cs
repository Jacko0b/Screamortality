using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    private static float batteryLevel;
    private static bool firstFlashlightPickup = true;
    private static bool[] activatedSymbols = new bool[24];

    public static bool closedDoor = false;
    public static bool monsterNoticed = false;
    public static bool symbolNoticed = false;
    public static bool symbolSolved = false;


    public static float BatteryLevel { get => batteryLevel; set => batteryLevel = value; }
    public static bool FirstFlashlightPickup { get => firstFlashlightPickup; set => firstFlashlightPickup = value; }
    public static bool[] ActivatedSymbols { get => activatedSymbols; set => activatedSymbols = value; }


}
