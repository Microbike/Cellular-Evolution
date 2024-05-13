using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chemosynthesis : Mutation
{
    public float energyGainSpeed = 5;
    public float energyDepletionSpeed = 1;
    public float chemicalDepletionRange = 2;
    private void Start()
    {

    }

    private void Update()
    {
        if(_cellController.alive){
            int numNearbyCells = 0;
            foreach (float sqrDist in myCell.cellsInRange)
            {
                if (sqrDist < (chemicalDepletionRange * chemicalDepletionRange)){
                    numNearbyCells ++;
                }
            }
            float nearbyChemicalDepletion = 1 + numNearbyCells / 2;
            _cellController.energy += Time.deltaTime * (energyGainSpeed / nearbyChemicalDepletion - energyDepletionSpeed) ;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, chemicalDepletionRange);
    }


}
