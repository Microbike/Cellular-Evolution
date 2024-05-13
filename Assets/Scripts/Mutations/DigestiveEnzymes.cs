using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigestiveEnzymes : Mutation
{
    public float lifeSappingSpeed = 2;
    public float energyGainSpeed = 10;
    public float lifeDepletionSpeed = 0.2f;
    private void Start()
    {

    }

    private void Update()
    {
        if(_cellController.alive){
            List<CellController> gettingSapped = new();
            int numTouchingCells = 0;
            for (int i = 0; i < myCell.cellsInRange.Count; i++)
            {
                if (myCell.cellsInRange[i] < 0){
                    numTouchingCells ++;
                    CellController sappedCellController = myCell.cellsInRange2[i].GetComponent<CellController>();
                    gettingSapped.Add(sappedCellController);
                    sappedCellController.life -= lifeSappingSpeed;
                }

            }
            _cellController.energy += Time.deltaTime * energyGainSpeed * numTouchingCells ;
            _cellController.life -= Time.deltaTime * lifeDepletionSpeed;
        }
    }
}