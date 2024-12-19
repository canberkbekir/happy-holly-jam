using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    [SerializeField] private Rigidbody projectile;
    private Vector3 startingPosition;
    
    
    [SerializeField] private float launchSpeed;
    [SerializeField] private int details;
    [SerializeField] private int drawDistance;
    
    private Vector3 Position => transform.position;
    private Vector3 Velocity => transform.forward * launchSpeed;
    private Vector3 Acceleration => Physics.gravity;

    
    private readonly List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        startingPosition = projectile.position;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LaunchObject();
        if (Input.GetKeyDown(KeyCode.Q))
            ResetObject();

    }
    
    private void LaunchObject()
    {
        projectile.linearVelocity = Velocity;
        projectile.useGravity = true;
    }

    private void ResetObject()
    {
        projectile.linearVelocity = Vector3.zero;
        projectile.position = startingPosition;        
        projectile.useGravity = false;
        
    }
    
    private void OnDrawGizmos()
    {
        positions.Clear();

        for (int i = 0; i < details; i++)
        {
            float t = i / (float)details;
            t *= drawDistance;
            positions.Add(GetPosition(t));
        }
        
        for (int i = 1; i < positions.Count; i++)
            Gizmos.DrawLine(positions[i - 1], positions[i]);
    }

    private Vector3 GetPosition(float t) => Position + Velocity * t + (Acceleration / 2) * t * t;
}
