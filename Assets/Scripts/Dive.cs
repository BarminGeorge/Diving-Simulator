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
            TakeTook();
        else if (Input.GetKey(KeyCode.W))
            TakePike();
        else if (Input.GetKey(KeyCode.E))
            DoTwists();
        else 
            PutTheEntrance();
    }

    private void TakeTook() => TakePosition(classOfDive.TookPosition, Constants.TookSpeedRotation);

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
}