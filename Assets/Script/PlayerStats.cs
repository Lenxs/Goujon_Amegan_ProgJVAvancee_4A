using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int lifeNumber = 8;
    [SerializeField] int maxLife = 8;
    [SerializeField] int maxAmmo = 5;
    [SerializeField] int currentAmmo = 3;

    private void Start()
    {
        lifeNumber = maxLife;
    }
    public void LostLife()
    {
        lifeNumber = lifeNumber - 1;
    }

    public int GetMaxLife()
    {
        return maxLife;
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

    public void GainAmmo()
    {
        if(currentAmmo < maxAmmo)
        {
            currentAmmo++;
        }
        else
        {
            currentAmmo = maxAmmo;
        }
        
    }

}
