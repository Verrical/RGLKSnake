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
            GameManager.Instance.AddScore(100);
            SpawnFood(snake.GetOccupiedCells());
        }
    }

    void SpawnFood(List<Vector2Int> occupied)
    {
        Vector2Int pos;
        pos = GridManager.Instance.GetRandomCell();
        foodGridPos = pos;
        transform.position = GridManager.Instance.GridToWorld(pos);
    }
}