using UnityEngine;
using System;

public class CellController : MonoBehaviour
{
    [NonSerialized] public float horizontalInput;
    [NonSerialized] public float verticalInput;

    private void Awake()
    {
    }
    public virtual void Update()
    {

    }

    // Function to initiate an Split
    public virtual void Split()
    {
        GameObject newCell = Instantiate(this.gameObject);
        PlayerController pc = newCell.GetComponent<PlayerController>();
        if(pc != null)
        {
            Destroy(pc);
            newCell.AddComponent<AIController>();
        }
    }
}
