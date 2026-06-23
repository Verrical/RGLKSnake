using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public static FoodSpawner Instance;

    private Vector2Int foodGridPos;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpawnFood(null);
    }

    public void CheckEat(Vector2Int headPos, SnakeController snake)
    {
        if (headPos == foodGridPos)
        {
            snake.Grow();
            GameManager.Instance.AddScore();
            SpawnFood(snake.GetOccupiedCells());
        }
    }

    void SpawnFood(List<Vector2Int> occupied)
    {
        Vector2Int pos;
        int attempts = 0;

        // Find a free cell
        do {
            pos = GridManager.Instance.GetRandomCell();
            attempts++;
        } while (occupied != null && occupied.Contains(pos) && attempts < 200);

        foodGridPos = pos;
        transform.position = GridManager.Instance.GridToWorld(pos);
    }
}