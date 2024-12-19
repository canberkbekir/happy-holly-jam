using System;
using Globals;
using HouseBuilding.Base;
using UnityEngine;
using Random = UnityEngine.Random;

public class HouseLauncher : MonoBehaviour
{
    private Trajectory trajectory;

    private Rigidbody currentHouseRigidbody;
    [SerializeField] private float angularVelocityStrength;
    
    private void OnEnable()
    {
        GameManager.Instance.HouseSpawnManager.OnHouseSpawned += SubscribeToHouseBuild;
    }

    private void SubscribeToHouseBuild(HouseChecker checker)
    {
        currentHouseRigidbody = checker.GetComponent<Rigidbody>();
        checker.HouseBuilt += ThrowObject;
    }
    
    private void ThrowObject()
    {
        currentHouseRigidbody.linearVelocity = trajectory.Velocity;
        currentHouseRigidbody.angularVelocity = GetAngularVelocity();
        currentHouseRigidbody.useGravity = true;
    }

    private Vector3 GetAngularVelocity()
    {
        return new Vector3(Random.Range(0,angularVelocityStrength), Random.Range(0,angularVelocityStrength), Random.Range(0,angularVelocityStrength));
    }
}

