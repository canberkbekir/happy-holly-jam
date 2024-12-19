using System;
using System.Collections;
using System.Collections.Generic;
using Globals;
using HouseBuilding.Base;
using UnityEngine;
using Random = UnityEngine.Random;

public class HouseSpawnManager : MonoBehaviour
{
    
    public event Action<HouseChecker> OnHouseSpawned;
    
    [SerializeField] private List<GameObject> houseToSpawn;

    [SerializeField] private Transform housePivot;

    [SerializeField] private int houseSpawnCount;

    private GameObject currentHouse;
    
    private bool skipInTheBeginning = true;
    
    private void OnEnable()
    {
        GameManager.Instance.RestartGame += RestartGame;
        GameManager.Instance.SatisfactionController.OnSatisfactionIsZero += ResetBool;
    }

    private void ResetBool()
    {
        skipInTheBeginning = true;
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

        StartCoroutine(WaitABitAndSpawn());
    }

    private IEnumerator WaitABitAndSpawn()
    {
        yield return new WaitForSeconds(skipInTheBeginning ? 0 : 1);
        skipInTheBeginning = false;
        
        houseSpawnCount--;

        int randomIndex = Random.Range(0, houseToSpawn.Count);
        currentHouse = Instantiate(houseToSpawn[randomIndex], housePivot);
        HouseChecker houseChecker = currentHouse.GetComponent<HouseChecker>();
        
        OnHouseSpawned!.Invoke(houseChecker);
        
        houseChecker.HouseBuilt += SpawnHouse;
    }

    private void RestartGame()
    {
        if (currentHouse != null)
            Destroy(currentHouse);
        
        SpawnHouseWave();
    }
    
}
