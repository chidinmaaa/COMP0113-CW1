using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnBot : MonoBehaviour
{
    public GameObject[] bots;
    public int num_bots;

    void Start()
    {
        num_bots = bots.Length;
        InvokeRepeating("Spawn", 3.0f, 7.0f);
    }

    void Update()
    {}

    void Spawn()
    {
        int id = Random.Range(0, num_bots);
        GameObject bot = bots[id];
        Instantiate(bot, gameObject.transform);
        //Debug.Log("Spawned " + bot.name);
    }
}