using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagellum : Mutation
{
    
    public float moveSpeed;
    public float rotationSpeed;

    private void Start()
    {
    }

    private void Update()
    {
        if(_cellController.alive){
            Vector2 velChange = myCell.gameObject.transform.up * _cellController.verticalInput * moveSpeed;
            myCell.velocity += velChange;
            myCell.rotationVelocity -= _cellController.horizontalInput * rotationSpeed;
            _cellController.energy -= Time.deltaTime * ((Mathf.Abs(_cellController.verticalInput) * 2) + (Mathf.Abs(_cellController.horizontalInput) * 1));
        }
    }

}
