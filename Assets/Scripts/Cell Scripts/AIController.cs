using UnityEngine;

public class AIController : CellController
{
    // Function to handle AI behavior
    public void Update()
    {
        horizontalInput = Random.Range(-1f, 1f);
        verticalInput =  Random.Range(-1f, 1f);
    }
}
