using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Player
{
    Player1,
    Player2
}

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] PlayerStats p1Stats, p2Stats;
    [SerializeField] Text lifeP1, lifeP2;


    private void Update()
    {
        lifeP1.text = p1Stats.GetLife().ToString();
        lifeP2.text = p2Stats.GetLife().ToString();

    }

    public void PlayerDeath(GameObject playerToRespawn)
    {
        playerToRespawn.GetComponent<PlayerStats>().LostLife();
        if (playerToRespawn.GetComponent<PlayerStats>().GetLife() > 0)
        {
            Respawn(playerToRespawn);
        }
        else
        {
            Debug.Log("Fin de partie");
        }
        
    }

    void Respawn(GameObject playerToRespawn)
    {
        int i = Random.Range(0, spawnPoint.Length - 1);
        playerToRespawn.transform.position = spawnPoint[i].position;
    }
}
