using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mitosis : Mutation
{
    
    public float requiredEnergy;
    public float healthDepletion;
    private GameObject splitButton;
    private void Start()
    {
        _cellController.onSplit.AddListener(OnSplit);
        splitButton = GameObject.FindWithTag("SplitButton");

    }

    private void Update()
    {
        if(_cellController.alive){
            _cellController.life -= healthDepletion * Time.deltaTime;
            if(_cellController.energy < requiredEnergy){
                
                if(_cellController.amPlayerController)
                    splitButton.SetActive(false);
            }else{
                if(_cellController.amPlayerController)
                    splitButton.SetActive(true);
            }
        }
    }

    private void OnSplit()
    {
        if(_cellController.alive){
            if(_cellController.energy >= requiredEnergy)
            {
                _cellController.AwakeCell();
                GameObject newCell = Instantiate(this.gameObject);
                newCell.transform.position += new Vector3 (UnityEngine.Random.Range(-0.001f, 0.001f),UnityEngine.Random.Range(-0.001f, 0.001f),0);
                CellInputs inputSystem = newCell.GetComponent<CellInputs>();
                if(inputSystem != null)
                {
                    inputSystem.amPlayerController = false;
                }
            }
        }
    }


}
