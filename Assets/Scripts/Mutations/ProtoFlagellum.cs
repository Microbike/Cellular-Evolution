using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoFlagellum : Mutation
{
    private CellController cellController;
    private SoftCollision cell;
    public float moveSpeed;
    public float rotationSpeed;

    private void Start()
    {
        cellController = GetComponent<CellController>();
        cell = GetComponent<SoftCollision>();
    }

    private void FixedUpdate()
    {
        cell.velocity += new Vector2(cellController.horizontalInput * rotationSpeed, cellController.verticalInput * moveSpeed);
    }

}
