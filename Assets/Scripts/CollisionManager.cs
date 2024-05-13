using UnityEngine;
using System;
using System.Collections.Generic;

public class CollisionManager : MonoBehaviour
{
    public float pushForce = 0.1f;
    public int gridCellSize = 10;
    public Vector2Int worldSize;
    public int collisionChecks, collisionEvents;
    public List<SoftCollision> allCells;
    private Dictionary<Vector2Int, List<SoftCollision>> grid = new Dictionary<Vector2Int, List<SoftCollision>>();

    private void Start()
    {
        // Initialize the grid
        // allCells.AddRange(FindObjectsOfType<SoftCollision>()); initialization is perfomed by each cell
        // UpdateGridCells();
    }

    private void Update ()
    {
        //collisionEvents = 0;
        //collisionChecks = 0;
        // NonPartitioned CollisionCheck
        // foreach(SoftCollision cell in allCells)
        // {
        //     foreach(SoftCollision otherCell in allCells)
        //     {
        //         collisionChecks ++;
        //         if(CellsTouching(cell, otherCell))
        //             {
        //                 print("telling cells to collide");
        //                 cell.Collide(otherCell);
        //                 collisionEvents ++;
        //             }
        //     }
        // }
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
        if (!allCells.Contains(cell))
        {
            allCells.Add(cell);
        }
        else
        {
            Debug.Log("Adding " + cell.gameObject.name + " to grid failed, grid already has it");
        }
    }
    // Place each cell into appropriate grid cell
    private void UpdateGridCells()
    {
        grid.Clear();
        foreach (SoftCollision cell in allCells)
        {
            Vector2Int cellIndex = GetCellIndex(cell.transform.position);
            if (!grid.ContainsKey(cellIndex))
            {
                grid[cellIndex] = new List<SoftCollision>();
            }
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
        Vector2Int cellIndex = GetCellIndex(cell.transform.position);
        cell.cellsInRange.Clear();
        cell.cellsInRange2.Clear();
        // Check cells in neighboring cells (cardinal directions only)
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // Skip diagonal neighbors
                // if (x != 0 && y != 0)
                //     continue;

                Vector2Int neighborCellIndex = cellIndex + new Vector2Int(x, y);
                if (grid.ContainsKey(neighborCellIndex))
                {
                    foreach (SoftCollision otherCell in grid[neighborCellIndex])
                    {

                        if (otherCell != cell)
                        {
                            float r0 = cell.radius;
                            float r1 = otherCell.radius;

                            Vector2 transformOffset = cell.transform.position - otherCell.transform.position;
                            float squareDist = transformOffset.sqrMagnitude - ((r0 * r0) + (r1 * r1));
                            if(otherCell.alive){
                                cell.cellsInRange.Add(squareDist);
                                cell.cellsInRange2.Add(otherCell);
                            }
                            cell.Collision(transformOffset, squareDist, otherCell.mass);
                            otherCell.Collision(-transformOffset, squareDist, cell.mass);
                            //print("in similar gridCells");
                            /*Vector2 position1 = cell.gameObject.transform.position;
                            Vector2 position2 = otherCell.gameObject.transform.position;
                            if (CellsTouching(cell, otherCell, position1, position2))
                            {
                                print("telling cells to collide");
                                if(position1.x >= position2.x)
                                {
                                otherCell.RecieveCollision(cell.Collide(otherCell));
                                collisionEvents++;
                                }
                            }*/
                            
                        }
                    }
                }
            }
        }
    }


    public bool CellsTouching(SoftCollision cell1, SoftCollision cell2, Vector2 position1, Vector2 position2)
    {
        collisionChecks++;
        float radiusSum = cell1.radius + cell2.radius;
        

        // Squared distance comparison to avoid square root calculation
        float squaredDistance = (position1 - position2).sqrMagnitude;
        float squaredRadiusSum = radiusSum * radiusSum;
        return squaredDistance <= squaredRadiusSum;
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3 (worldSize.x, worldSize.y, 0) * gridCellSize);
    }
    private void OnDrawGizmosSelected() {
        Vector3 pos0 = new Vector3();
        Vector3 pos1 = new Vector3();
        for (int i = -worldSize.x/2; i < worldSize.x/2; i++)
        {
            pos0.x = i;
            pos0.y = -worldSize.y/2;
            pos1.x = i;
            pos1.y = worldSize.y/2;
            Gizmos.DrawLine( pos0 * gridCellSize, pos1 * gridCellSize);
        }

        for (int i = -worldSize.y/2; i < worldSize.y/2; i++)
        {
            pos0.x = -worldSize.x/2;
            pos0.y = i;
            pos1.x = worldSize.x/2;
            pos1.y = i;
            Gizmos.DrawLine(pos0 * gridCellSize, pos1 * gridCellSize);
        }
    }

}