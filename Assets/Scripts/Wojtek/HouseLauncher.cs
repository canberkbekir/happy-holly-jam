using System;
using Globals;
using HouseBuilding.Base;
using UnityEngine;
using Random = UnityEngine.Random;

public class HouseLauncher : MonoBehaviour
{
    [SerializeField] private Trajectory trajectory;

    private GameObject currentObject;
    [SerializeField] private float angularVelocityStrength;
    
    private void OnEnable()
    {
        GameManager.Instance.HouseSpawnManager.OnHouseSpawned += SubscribeToHouseBuild;
    }

    private void SubscribeToHouseBuild(HouseChecker checker)
    {
        currentObject = checker.gameObject;
        checker.HouseBuilt += ThrowObject;
    }
    
    private void ThrowObject()
    {
        Rigidbody houseRB = currentObject.AddComponent<Rigidbody>();
        
        houseRB.isKinematic = false;
        houseRB.useGravity = true;
        
        houseRB.linearVelocity = trajectory.Velocity;
        houseRB.angularVelocity = GetAngularVelocity();
    }

    private Vector3 GetAngularVelocity()
    {
        return new Vector3(Random.Range(0,angularVelocityStrength), Random.Range(0,angularVelocityStrength), Random.Range(0,angularVelocityStrength));
    }
}

