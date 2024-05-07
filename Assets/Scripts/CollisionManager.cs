using UnityEngine;
using System;
using System.Collections.Generic;

public class CollisionManager : MonoBehaviour
{
    public float pushForce = 0.1f;
    public int gridCellSize = 10;
    public float worldSizeX, worldSizeY;
    public int collisionChecks;
    private List<SoftCollision> allCells;
    private Dictionary<Vector2Int, List<SoftCollision>> grid = new Dictionary<Vector2Int, List<SoftCollision>>();

    private void Start()
    {
        // Initialize the grid
        allCells.AddRange(FindObjectsOfType<SoftCollision>());
        UpdateGridCells();
    }
    private void FixedUpdate()
    {
        UpdateGridCells();
        foreach (List<SoftCollision> cells in grid.Values)
        {
            foreach (SoftCollision cell in cells)
            {
                CheckCollisionsOf(cell);
            }
        }

    }
    public void AddCellToGrid(SoftCollision cell)
    {
        allCells.Add(cell);
    }
    // Place each cell into appropriate grid cell
    private void UpdateGridCells()
    {
        grid.Clear();
        foreach (SoftCollision cell in allCells)
        {
            Vector2Int cellIndex = GetCellIndex(cell.transform.position);
            grid[cellIndex].Add(cell);
        }
    }

    private Vector2Int GetCellIndex(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / gridCellSize),
            Mathf.FloorToInt(position.y / gridCellSize)
        );
    }

    private void CheckCollisionsOf(SoftCollision cell)
    {
        //print("checking all collisions in one cell");
        Vector2Int cellIndex = GetCellIndex(cell.transform.position);
        // Check cells in neighboring cells
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int neighborCellIndex = cellIndex + new Vector2Int(x, y);
                if (grid.ContainsKey(neighborCellIndex))
                {
                    foreach (SoftCollision otherCell in grid[neighborCellIndex])
                    {
                        if (otherCell != cell)
                        {
                            //print("cells in range");
                            collisionChecks ++;
                            if(cellsTouching(cell, otherCell))
                            {
                                print("telling cells to collide");
                                cell.Collide(otherCell);
                            }
                        }
                    }
                }
            }
        }
    }

    public bool cellsTouching(SoftCollision cell1, SoftCollision cell2)
    {
        float radius1 = cell1.radius;
        float radius2 = cell2.radius;
        Vector2 position1 = cell1.gameObject.transform.position;
        Vector2 position2 = cell2.gameObject.transform.position;

        float distance = Vector2.Distance(position1, position2);
        return distance <= radius1 + radius2;
    }


}
