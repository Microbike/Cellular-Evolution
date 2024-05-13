using UnityEngine;
using System;
using UnityEngine.Events;

public class CellController : MonoBehaviour
{
    [NonSerialized] public float horizontalInput;
    [NonSerialized] public float verticalInput;
    public float energy;
    public float maxEnergy;
    public float life;
    public float lifespan;
    public SpriteRenderer mySprite;
    public UnityEvent onSplit;
    public bool amPlayerController;
    public bool alive;

    public void AwakeCell()
    {
        life = lifespan;
        energy = 0;
    }

    public virtual void Split()
    {
        onSplit.Invoke();
    }
}
