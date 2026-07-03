using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public GameObject bodySegmentPrefab;
    public Transform bodyContainer;
    public float moveInterval = 0.1f;
    public float cooldown = 0.5f;
    public float dodgetime = 0.3f;


    private Vector2Int gridPos;
    private Vector2Int direction = Vector2Int.right;
    private Vector2Int nextDirection = Vector2Int.right;
    private List<Transform> bodySegments = new List<Transform>();
    private List<Vector2Int> segmentPositions = new List<Vector2Int>();
    private float moveTimer;
    private bool alive = true;
    private float cooldownTimer = 0f;
    private float iframes = 0f;

    void Start()
    {
        gridPos = new Vector2Int(
            GridManager.Instance.width / 2,
            GridManager.Instance.height / 2
        );
        transform.position = GridManager.Instance.GridToWorld(gridPos);
        segmentPositions.Add(gridPos);
    }

    void Update()
    {
        if (!alive) return;

        HandleInput();

        if (cooldownTimer > 0f)
        cooldownTimer -= Time.deltaTime;
        if (iframes > 0f)
        iframes -= Time.deltaTime;
        
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            moveTimer = 0f;
            Move();
        }
    }
    public bool cooldownCheck()
    {
        return iframes > 0f;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            if (direction != Vector2Int.down) nextDirection = Vector2Int.up;

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            if (direction != Vector2Int.up) nextDirection = Vector2Int.down;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            if (direction != Vector2Int.right) nextDirection = Vector2Int.left;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            if (direction != Vector2Int.left) nextDirection = Vector2Int.right;
        if (Input.GetKeyDown(KeyCode.E) && cooldownTimer <= 0)
            Ability();    
    }

    void Move()
    {
        direction = nextDirection;
        Vector2Int newPos = gridPos + direction;
        if (!GridManager.Instance.IsInBounds(newPos))
        {
            Die();
            return;
        }

        for (int i = 1; i < segmentPositions.Count; i++)
        {
            if (segmentPositions[i] == newPos)
            {
                Die();
                return;
            }
        }
        gridPos = newPos;
        transform.position = GridManager.Instance.GridToWorld(gridPos);

        segmentPositions.Insert(0, gridPos);
        segmentPositions.RemoveAt(segmentPositions.Count - 1);

        for (int i = 0; i < bodySegments.Count; i++) {
            bodySegments[i].position = GridManager.Instance.GridToWorld(segmentPositions[i + 1]);
        }
        FoodSpawner.Instance.CheckEat(gridPos, this);

        if (ObstacleSpawner.Instance.IsObstacle(newPos)) {
            if (cooldownCheck() == true)
            {
                ObstacleSpawner.Instance.DestroyObstacle(newPos);
                GameManager.Instance.AddScore(70);
            }
            else
            {
                Die();
            }

        }
    }

    public void Grow()
    {
        Vector2Int tailPos = segmentPositions[segmentPositions.Count - 1];
        GameObject seg = Instantiate(bodySegmentPrefab,
            GridManager.Instance.GridToWorld(tailPos), Quaternion.identity, bodyContainer);
        bodySegments.Add(seg.transform);
        segmentPositions.Add(tailPos);
    }

    void Die()
    {
        alive = false;
        GameManager.Instance.GameOver();
    }

    public List<Vector2Int> GetOccupiedCells()
    {
        return segmentPositions;
    }
    public void Ability()
{
    if (bodySegments.Count == 0) return;

    int lastIndex = bodySegments.Count - 1;
    Destroy(bodySegments[lastIndex].gameObject);
    bodySegments.RemoveAt(lastIndex);
    segmentPositions.RemoveAt(segmentPositions.Count - 1);
    cooldownTimer = cooldown;
    iframes = dodgetime;
    GameManager.Instance.AddScore(-20);
}
}
