using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues
{
    public static float fireRateMelee = 0.3f;

    public static float fireRatePistol = 0.25f;
    public static int ammoCountPistol = 10;


    public static float fireRateRifle = 0.1f;
    public static int ammoCountRifle = 30;

    public static float fireRateShotgun = 0.8f;
    public static int ammoCountShotgun = 5;
    public static float spreadShotgun = 20;



}

public enum WeaponType
{
    Knife = 0,
    Pistol = 1,
    Rifle = 2,
    Shotgun = 3
};