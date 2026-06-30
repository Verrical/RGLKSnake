using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    [Header("References")]
    public GameObject obstaclePrefab;

    public int obstacleCount = 1;

    private List<Vector2Int> obstaclePositions = new List<Vector2Int>();
    private List<GameObject> spawnedObstacles = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2Int pos = GetFreeCell();
            obstaclePositions.Add(pos);

            GameObject obs = Instantiate(obstaclePrefab,
                GridManager.Instance.GridToWorld(pos), Quaternion.identity, transform);
            spawnedObstacles.Add(obs);
        }
    }
    Vector2Int GetFreeCell()
    {
        Vector2Int pos;
        int attempts = 0;
        Vector2Int center = new Vector2Int(GridManager.Instance.width / 2, GridManager.Instance.height / 2);

        do
        {
            pos = GridManager.Instance.GetRandomCell();
            attempts++;
        }
        while ((obstaclePositions.Contains(pos) || pos == center) && attempts < 200);

        return pos;
    }


    public bool IsObstacle(Vector2Int gridPos)
    {
        return obstaclePositions.Contains(gridPos);

    }
    
    public void DestroyObstacle(Vector2Int gridPos)
    {
        int index = obstaclePositions.IndexOf(gridPos);
        if (index == -1) return;

        Destroy(spawnedObstacles[index]);
        obstaclePositions.RemoveAt(index);
        spawnedObstacles.RemoveAt(index);
    }

    
}