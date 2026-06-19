using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")]
    public int width = 30;
    public int height = 15;
    public float cellSize = 1f;


    [Header("Camera Settings")]
    public float cameraPadding = 1.5f;  

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FitCameraToGrid();
    }

    void FitCameraToGrid()
    {
        Camera cam = Camera.main;

        // Center the camera on the grid
        float centerX = (width * cellSize) / 2f;
        float centerY = (height * cellSize) / 2f;
        cam.transform.position = new Vector3(centerX, centerY, -10f);

        // Fit orthographic size to whichever dimension is larger
        float verticalSize = (height * cellSize) / 2f + cameraPadding;
        float horizontalSize = (width * cellSize) / 2f + cameraPadding;
        float aspectRatio = (float)Screen.width / Screen.height;

         cam.orthographicSize = Mathf.Max(verticalSize, horizontalSize / aspectRatio);
    }
    // Convert a grid coordinate to a world position
    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * cellSize, gridPos.y * cellSize, 0);
    }

    // Check if a grid position is within bounds
    public bool IsInBounds(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < width &&
               gridPos.y >= 0 && gridPos.y < height;
    }

    // Get a random grid position (used by food spawner)
    public Vector2Int GetRandomCell()
    {
        return new Vector2Int(Random.Range(0, width), Random.Range(0, height));
    }
}