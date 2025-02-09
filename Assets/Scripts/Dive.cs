using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dive : Sounds
{
    public SpriteCollection Collection;
    public Transform transform;
    public BoxCollider2D BoxCollider;
    public Vector2 forwardDirection;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public bool canJump = true;
    public ClassOfDive classOfDive = new ClassOfDive(DiveType.None);
    public float totalFlips = 0f;
    private float rotationSpeedAndDirection;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChooseClassOfDive();
        Jump();
        ChoosePosition();
        Spin();
        CountFlips();
        TryChooseSound(transform, forwardDirection);
    }
    
    private void ChooseClassOfDive()
    {
        if (!canJump) 
            return;
        if (Input.GetKeyDown(KeyCode.Q))
            classOfDive = new ClassOfDive(Collection, DiveType.FirstClass, transform, BoxCollider, spriteRenderer);
        else if (Input.GetKeyDown(KeyCode.W))
            classOfDive = new ClassOfDive(Collection, DiveType.SecondClass, transform, BoxCollider, spriteRenderer);
        else if (Input.GetKeyDown(KeyCode.E))
            classOfDive = new ClassOfDive(Collection, DiveType.ThirdClass, transform, BoxCollider, spriteRenderer);
        else if (Input.GetKeyDown(KeyCode.R))
            classOfDive = new ClassOfDive(Collection, DiveType.FourthClass, transform, BoxCollider, spriteRenderer);
        else if (Input.GetKeyDown(KeyCode.T))
            classOfDive = new ClassOfDive(Collection, DiveType.SixthFrontClass, transform, BoxCollider, spriteRenderer);
        else if (Input.GetKeyDown(KeyCode.Y))
            classOfDive = new ClassOfDive(Collection, DiveType.SixthBackClass, transform, BoxCollider, spriteRenderer);
        else if (Input.GetKeyDown(KeyCode.U))
            classOfDive = new ClassOfDive(Collection, DiveType.SixthThirdClass, transform, BoxCollider, spriteRenderer);
    }
    
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !(classOfDive.SelectedClassOfDive is DiveType.None))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x + Constants.DeltaXInJump, classOfDive.JumpForce);
            canJump = false;
        }
    }

    private void Spin()
    {
        if (!canJump && transform.position.y >= Constants.LevelWater)
            transform.Rotate(0, 0, rotationSpeedAndDirection * Time.deltaTime);
        else 
            transform.Rotate(0, 0, 0);
        forwardDirection = transform.up;
    }
    
    private void ChoosePosition()
    {
        if (canJump) 
            return;
        if (Input.GetKey(KeyCode.Q)) 
            TakeTuck();
        else if (Input.GetKey(KeyCode.W))
            TakePike();
        else if (Input.GetKey(KeyCode.E))
            DoTwists();
        else 
            PutTheEntrance();
    }

    private void TakeTuck() => TakePosition(classOfDive.TuckPosition, Constants.TuckSpeedRotation);

    private void TakePike() => TakePosition(classOfDive.PikePosition, Constants.PikeSpeedRotation);
    
    // TODO
    private void DoTwists()
    {
        
    }

    private void PutTheEntrance() => TakePosition(classOfDive.EntrancePosition, Constants.EntranceSpeedRotation);

    private void TakePosition(Sprite sprite, float speed)
    {
        spriteRenderer.sprite = sprite;
        rotationSpeedAndDirection = classOfDive.RotateDirection * speed;
    }

    private void CountFlips()
    {
        if (canJump || transform.position.y < Constants.LevelWater) 
            return;
        var currentAngle = Mathf.Atan2(forwardDirection.y, forwardDirection.x) * Mathf.Rad2Deg;
        var diveAngle = Mathf.Atan2(classOfDive.forwardDirection.y, classOfDive.forwardDirection.x) * Mathf.Rad2Deg;
        var angleDifference = currentAngle - diveAngle;
        
        if (angleDifference < -180f)
            angleDifference += 360f;
        else if (angleDifference > 180f)
            angleDifference -= 360f;
        
        totalFlips += angleDifference / 360f;
        classOfDive.forwardDirection = forwardDirection;
    }
}