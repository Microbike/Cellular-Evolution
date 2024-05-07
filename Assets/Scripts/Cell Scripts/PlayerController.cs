using UnityEngine;

public class PlayerController : CellController
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Split();
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

    }
}
