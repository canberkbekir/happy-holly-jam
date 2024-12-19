using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class GridPlacer : MonoBehaviour
{
    [SerializeField] private GameObject visualizeObject;
    [SerializeField] private Material gridMaterial;

    [SerializeField] private List<GameObject> objectsToPlace = new List<GameObject>();
    [SerializeField] private int currentIndex;
    
    [SerializeField] private float gridSizeX;
    [SerializeField] private float gridSizeZ;

    private const string GRID_SIZE = "_GridSize";
    private const string GRID_OFFSET = "_Offset";
    
    private static readonly int GridSize = Shader.PropertyToID(GRID_SIZE);
    private static readonly int GridOffset = Shader.PropertyToID(GRID_OFFSET);

    private Vector3 currentPosition;
    private Vector3 currentScale;
    
    private void Awake()
    {
        SpawnObject();
    }


    private void Update()
    {
        ChooseObject();
        UpdateObjectPositionAndScale();
    }

    private void ChooseObject()
    {
       
        ChangeCurrentObject(KeyCode.F1,0);
        ChangeCurrentObject(KeyCode.F2, 1);
        ChangeCurrentObject(KeyCode.F3, 2);

    }

    private void ChangeCurrentObject(KeyCode keycode, int index)
    {
        if (Input.GetKeyDown(keycode))
        {
            this.currentIndex = index;
            SpawnObject();
        }
    }
    
    private void UpdateObjectPositionAndScale()
    {
        Ray ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
            SetObjectPosition(hit);

        if (Input.GetKeyDown(KeyCode.Space))
            PlaceObject();
    }

    private void PlaceObject()
    {
        if (visualizeObject != null)
            visualizeObject.layer = LayerMask.NameToLayer("Default");

        SpawnObject();
    }

    private void SetObjectPosition(RaycastHit hit)
    {
        if (visualizeObject == null)
            return;
        
        Vector3 finalPosition = hit.point + hit.normal * 0.5f;
        
        float x = finalPosition.x.Round(gridSizeX);
        float y;
        float z = finalPosition.z.Round(gridSizeZ);
        
        if (Mathf.Abs(hit.point.y - Mathf.Round(hit.point.y)) < 0.01f)
            y = Mathf.Round(hit.point.y);
        else
           y = Mathf.FloorToInt(hit.point.y);
        
        currentPosition = new Vector3(x, y + .5f, z);
        currentScale = new Vector3(1/gridSizeX, 1, 1/gridSizeZ);

        SetCalculateValues();
    }

    private void SetCalculateValues()
    {
        visualizeObject.transform.position = currentPosition;
        visualizeObject.transform.localScale = currentScale;
    }
    private void OnValidate()
    {
        Vector2 gridSize = new Vector2(gridSizeX * 10, gridSizeZ * 10);
        
        float dividerForGridOffsetX = 2 * gridSizeX;
        float dividerForGridOffsetZ = 2 * gridSizeZ;
        
        Vector2 gridOffset = new Vector2(gridSizeX / dividerForGridOffsetX, gridSizeZ / dividerForGridOffsetZ);
        
        gridMaterial.SetVector(GridSize, gridSize);
        gridMaterial.SetVector(GridOffset, gridOffset);
    
        UpdateObjectPositionAndScale();
    }

    private void SpawnObject()
    {
        if (visualizeObject != null)
        {
            visualizeObject = null;
            Destroy(visualizeObject);
        }
        
        visualizeObject = null;
        visualizeObject = Instantiate(objectsToPlace[currentIndex]);
    }
    
}