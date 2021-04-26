using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int round = 1;
    int zombiesInRound = 10;
    int zombiesSpawned = 0;
    public Transform[] spawnPoints;
    public GameObject zombie;
    public float spawnDelay = 15f;

    // Update is called once per frame
     void Update()
    {
        if (zombiesSpawned == zombiesInRound)
        {
            zombiesInRound += 10;
            zombiesSpawned = 0;
            round++;
        }
        if (round == 4)
        {
            //3 is victory scene
            SceneManager.LoadScene(3);
        }
    }
    void FixedUpdate()
    {
        StartCoroutine(RoundSpawner());
    }

    IEnumerator SpawnZombie()
    {
        if (zombiesSpawned < 3)
        {
            Vector3 randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            Instantiate(zombie, randomSpawn, Quaternion.identity);
            zombiesSpawned++;
            yield return new WaitForSeconds(5f);
        }
        else
        {
            Vector3 randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            Instantiate(zombie, randomSpawn, Quaternion.identity);
            zombiesSpawned++;
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator RoundSpawner()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (zombiesSpawned < zombiesInRound)
        {
            StartCoroutine(SpawnZombie());
        }
    }
}
