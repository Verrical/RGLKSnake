using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    [Header("References")]
    public GameObject obstaclePrefab;

    private Vector2Int obstaclePos;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnObstacle();
    }

    void SpawnObstacle()
    {
        Vector2Int center = new Vector2Int(GridManager.Instance.width / 2, GridManager.Instance.height / 2);
        Vector2Int pos;
        int attempts = 0;

        do
        {
            pos = GridManager.Instance.GetRandomCell();
            attempts++;
        }
        while (pos == center && attempts < 200); // avoid spawning on snake's start

        obstaclePos = pos;
        Instantiate(obstaclePrefab, GridManager.Instance.GridToWorld(pos), Quaternion.identity, transform);
    }

    public bool IsObstacle(Vector2Int gridPos)
    {
        return gridPos == obstaclePos;
    }
}