using System;
using System.Collections.Generic;
using Globals;
using HouseBuilding.Base;
using UnityEngine;
using Random = UnityEngine.Random;

public class HouseSpawnManager : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> houseToSpawn;

    [SerializeField] private Transform housePivot;

    [SerializeField] private int houseSpawnCount;

    private GameObject currentHouse;
    
    private void OnEnable()
    {
        GameManager.Instance.RestartGame += RestartGame;
    }
    
    public void SpawnHouseWave()
    {
        houseSpawnCount = Random.Range(1, houseToSpawn.Count);
        SpawnHouse();
    }

    private void SpawnHouse()
    {
        if (houseSpawnCount == 0)
        {
            SpawnHouseWave();
            return;
        }

        houseSpawnCount--;

        int randomIndex = Random.Range(0, houseToSpawn.Count);
        currentHouse = Instantiate(houseToSpawn[randomIndex], housePivot);
        HouseChecker houseChecker = currentHouse.GetComponent<HouseChecker>();
        houseChecker.HouseBuilt += SpawnHouse;
    }

    private void RestartGame()
    {
        if (currentHouse != null)
            Destroy(currentHouse);
        
        SpawnHouseWave();
    }
    
}
