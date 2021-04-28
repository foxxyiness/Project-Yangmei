using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int round = 1;
    public int zombiesInRound = 10;
    public int zombiesSpawned = 0;
    public static int zombiesLeft = 10;
    public Transform[] spawnPoints;
    public GameObject zombie;
    public float spawnDelay = 0f;
    public float zombiesSpawnTimer = 0f;
    public TextMeshProUGUI roundTimer;
    public TextMeshProUGUI roundNumber;

    // Update is called once per frame
     void Update()
    {
        roundTimer.SetText(Mathf.RoundToInt(spawnDelay).ToString());
        roundNumber.SetText(round.ToString());
    
        if(zombiesSpawned < zombiesInRound && spawnDelay == 0)
        {
            if(zombiesSpawnTimer > 2)
            {
                SpawnZombie();
                zombiesSpawnTimer = 0;
            }
            else
            {
                zombiesSpawnTimer += Time.deltaTime;
            }
        }
        else if(zombiesLeft == 0)
        {
            StartNextRound();
        }

        if(spawnDelay > 0)
        {
            spawnDelay -= Time.deltaTime;
            roundTimer.enabled = true;
        }
        else
        {
            spawnDelay = 0;
            roundTimer.enabled = false;
        }

        if (round == 4)
        {
            //3 is victory scene
            SceneManager.LoadScene(3);
        }


    }

    void StartNextRound()
    {
        zombiesInRound = zombiesLeft = round * 10;
        zombiesSpawned = 0;
        spawnDelay = 15f;
        round++;
    }

    void SpawnZombie()
    {
        Vector3 randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        Instantiate(zombie, randomSpawn, Quaternion.identity);
        zombiesSpawned++;
    }

   }
   