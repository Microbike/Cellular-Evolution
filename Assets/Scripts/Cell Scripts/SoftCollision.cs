using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftCollision : MonoBehaviour
{
    public float firmness;  
    public float mass = 1.0f; //0 for immovable objects
    public float radius = 1;
    public Vector2 velocity = Vector2.zero;
    public float damping = 0.1f;
    public float rotationVelocity;
    public float rotationDamping;
    public bool debug = false;

    private GameObject Gob;
    private CollisionManager _collisionManager;
    private Vector2 worldSize;
    private float worldCellSize;
    public bool alive;
    public List<float> cellsInRange;
    public List<SoftCollision> cellsInRange2;

    private void Start() 
    {
        alive = true;
        Gob = GameObject.FindWithTag("CollisionManager");
        _collisionManager = Gob.GetComponent<CollisionManager>();
        _collisionManager.AddCellToGrid(this);
        worldSize = _collisionManager.worldSize;
        worldCellSize = _collisionManager.gridCellSize;
    }
    private void Update()
    {
        velocity /= damping + 1;
        rotationVelocity /= rotationDamping + 1;
        Vector2 myPos = transform.position;
        Vector2 newPos = myPos + (velocity * Time.deltaTime);
        transform.Rotate(new Vector3(0,0,rotationVelocity * Time.deltaTime));
        
        if((newPos.x * 2)>worldSize.x * worldCellSize){
            newPos -= new Vector2(worldSize.x* worldCellSize,0);
        }
        if((newPos.x * 2)<-worldSize.x * worldCellSize){
            newPos += new Vector2(worldSize.x* worldCellSize,0);
        }
        if((newPos.y * 2)>worldSize.y * worldCellSize){
            newPos -= new Vector2(0, worldSize.y* worldCellSize);
        }
        if((newPos.y * 2)<-worldSize.y * worldCellSize){
            newPos += new Vector2(0, worldSize.y* worldCellSize);
        }
        transform.position = newPos;
    }
    
    public void Collision(Vector2 otherCellPos, float squareDist, float otherCellMass)
    {
        // if (squareDist < (range * range)){
        //     //print("in range");
        // }
        if (squareDist < 0)
        {
            float massMultiplier = otherCellMass/mass;
            velocity += otherCellPos * firmness * massMultiplier;
            //print("touching");
        }
    }
    
    
    /*public void RecieveCollision(Vector2 collision) //OLD
    {
        velocity -= collision * firmness;
    }
    public Vector2 Collide(SoftCollision otherCell) //OLD
    {
        //print("collided with" + otherCell.gameObject.name);
        float massMultiplier;
        //if(otherCell.mass == 0)
        //{
        //    massMultiplier = 1;
        //}else{
            massMultiplier = otherCell.mass/mass;
        //}
        Vector2 collisionDirection = (transform.position - otherCell.transform.position);
        if (collisionDirection != Vector2.zero)
        {
            collisionDirection /= collisionDirection.magnitude;
        }
        velocity += collisionDirection * firmness * massMultiplier;
        Vector3 returnVal = collisionDirection / massMultiplier;
        return(returnVal);
    }*/
}