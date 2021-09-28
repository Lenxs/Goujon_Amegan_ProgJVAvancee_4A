using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int lifeNumber = 8;
    [SerializeField] int maxAmmo = 5;
    [SerializeField] int currentAmmo = 3;

    public void LostLife()
    {
        lifeNumber = lifeNumber - 1;
    }

    public int GetLife()
    {
        return lifeNumber;
    }

    public int GetAmmo()
    {
        return currentAmmo;
    }

    public void LostAmmo()
    {
        currentAmmo--;
    }

}
