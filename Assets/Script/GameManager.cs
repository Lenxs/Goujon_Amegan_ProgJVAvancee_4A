using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Player
{
    Player1,
    Player2
}

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoint;
    [SerializeField] PlayerStats p1Stats, p2Stats;
    [SerializeField] Text endText,ammoP1,ammoP2;
    [SerializeField] Slider lifeP1, lifeP2;
    [SerializeField] GameObject EndGameUI;
    [SerializeField] int i;

    private void Start()
    {
        lifeP1.maxValue = p1Stats.GetMaxLife();
        lifeP2.maxValue = p2Stats.GetMaxLife();
    }
    private void Update()
    {
        ammoP1.text = p1Stats.GetAmmo().ToString() + " / 5 Arrow";
        ammoP2.text = p2Stats.GetAmmo().ToString() + " / 5 Arrow";
        lifeP1.value = p1Stats.GetLife();
        lifeP2.value = p2Stats.GetLife();
    }

    public void AmmoGain(GameObject player)
    {
        player.GetComponent<PlayerStats>().GainAmmo();
    }

    public void PlayerDeath(GameObject playerToRespawn)
    {
        playerToRespawn.GetComponent<PlayerStats>().LostLife();

        if (playerToRespawn.GetComponent<PlayerStats>().GetLife() > 0)
        {
            StartCoroutine(ResPlayer(playerToRespawn));
            if (playerToRespawn.name == "P2")
            {
                
                lifeP2.value -= 1;
            }else if(playerToRespawn.name == "P1")
            {
                
                lifeP1.value -= 1;
            }
        }
        else
        {
            EndGame();
        }
        
    }

    IEnumerator ResPlayer(GameObject playerToRespawn)
    {
        
        playerToRespawn.SetActive(false);
        yield return new WaitForSeconds(2f);
        playerToRespawn.SetActive(true);
        Respawn(playerToRespawn);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    private void EndGame()
    {
        EndGameUI.SetActive(true);
        if (p1Stats.GetLife() == 0)
        {
            endText.text = "Victoire du joueur 2";
        }else if (p2Stats.GetLife() == 0)
        {
            endText.text = "Victoire du joueur 1";
        }
    }

    void Respawn(GameObject playerToRespawn)
    {
        i = Random.Range(0, spawnPoint.Length - 1);
        playerToRespawn.transform.position = spawnPoint[i].position;
        
    }
}
