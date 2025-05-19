using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public float spacing = 2f;
    public GameObject cellPrefab;

    public float amplitude = 1f;
    public float stepTime = 0.01f;
    public float period = 3f;

    private GameObject[,] cells;
    private Vector2[,] initialPositions;
    private float[,] phases;
    private float time;

    void Start()
    {
        cells = new GameObject[width, height];
        initialPositions = new Vector2[width, height];
        phases = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x * spacing, y * spacing);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);

                cells[x, y] = cell;
                initialPositions[x, y] = pos;

                
                phases[x, y] = (x + y) * Mathf.PI / 4f; // ripple pattern
            }
        }
    }

    void Update()
    {
        time += stepTime;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 basePos = initialPositions[x, y];
                float phase = phases[x, y];
                float offsetY = amplitude * Mathf.Sin(2 * Mathf.PI * time / period + phase);
                Vector2 newPos = new Vector2(basePos.x, basePos.y + offsetY);

                cells[x, y].transform.position = newPos;
            }
        }
    }
}
