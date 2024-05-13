using UnityEngine;
using UnityEngine.UI;

public class CellInputs : CellController
{
    
    [Header("Player:")]
    private Slider energyBar;
    private Slider healthBar;
    [Header("AI:")]
    private Vector2 aiNoiseSamplePoint;
    public float aIBehaviourChangeSpeed = 1;
    private SoftCollision mySoftBody;
    
    private void Start()
    {   
        alive = true;
        base.AwakeCell();
        mySprite = GetComponent<SpriteRenderer>();
        mySoftBody = GetComponent<SoftCollision>();
        if(amPlayerController){
            energyBar = GameObject.FindWithTag("EnergyBar").GetComponent<Slider>();
            healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();
            //print("playerStart");
        }else{
            //print("aiStart");
            aiNoiseSamplePoint = new Vector2(Random.Range(-100,100),Random.Range(-100,100));
        }
    }
    public void Update()
    {
        if(alive){
            //life -= Time.deltaTime;
            
            if(energy > maxEnergy){
                energy = maxEnergy;
            }
            if(amPlayerController){
                PlayerUpdate();
            }else{
                AIUpdate();
            }

            if(life < 0){
                mySprite.color = new Color(0.7f,0.7f,0.7f);
                horizontalInput = 0;
                verticalInput = 0;
                mySoftBody.alive = false;
                alive = false;
            }
        }
    }
    private void PlayerUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Split();
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //print(horizontalInput);
        energyBar.value = energy / maxEnergy;
        healthBar.value = life / lifespan;
    }
    private void AIUpdate()
    {
        Mitosis reproductionMutation = GetComponent<Mitosis>();
        if (reproductionMutation != null)
        {
            if(energy >= reproductionMutation.requiredEnergy)
                Split();
        }
        float rotationNoiseData = Mathf.PerlinNoise(aiNoiseSamplePoint.x, 0);
        float movementNoiseData = Mathf.PerlinNoise(aiNoiseSamplePoint.y, 0);
        horizontalInput = Mathf.Clamp(Mathf.Pow(((rotationNoiseData-0.5f) * 16),3),-1,1);
        //print(rotationNoiseData + " -> " + horizontalInput);
        verticalInput = Mathf.Clamp(Mathf.Pow(((movementNoiseData-0.5f) * 4 + 0.2f),3),-1,1);
        aiNoiseSamplePoint += new Vector2(Time.deltaTime/2, Time.deltaTime) * aIBehaviourChangeSpeed;
    }
}
