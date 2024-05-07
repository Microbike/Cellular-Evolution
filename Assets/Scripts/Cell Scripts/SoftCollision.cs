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
    private void Start() 
    {
        GameObject Gob = GameObject.FindWithTag("CollisionManager");
        CollisionManager _collisionManager = Gob.GetComponent<CollisionManager>();
        if(_collisionManager == null)
        {
            print("null");
        }else{
            print("not null");
        }
        Debug.Log(Gob.name);
        print("wtf");
        _collisionManager.AddCellToGrid(this);
    }
    private void FixedUpdate()
    {
        velocity /= damping + 1;
        rotationVelocity /= rotationDamping + 1;
        transform.Translate(velocity * Time.fixedDeltaTime);
    }

    public void Collide(SoftCollision otherCell)
    {
        //print("collided with" + otherCell.gameObject.name);
        float massMultiplier;
        if(otherCell.mass == 0)
        {
            massMultiplier = 1;
        }else{
            massMultiplier = otherCell.mass/mass;
        }
        Vector2 collisionDirection = (transform.position - otherCell.transform.position).normalized;
        
        velocity += collisionDirection * massMultiplier * firmness;
    }

    private void OnDrawGizmosSelected()
    {
    }

}
