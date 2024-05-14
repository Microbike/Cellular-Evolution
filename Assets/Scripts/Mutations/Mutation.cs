using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutation : MonoBehaviour
{
    protected CellController _cellController;
    protected SoftCollision myCell;
    public string Name;
    [TextAreaAttribute]public string Description;
    public virtual void Awake()
    {
        _cellController = GetComponent<CellController>();
        myCell = GetComponent<SoftCollision>();
    }
}
