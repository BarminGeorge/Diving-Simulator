using UnityEngine;

public class ClassOfDive
{
    public byte JumpForce { get; private set; }
    public DiveType SelectedClassOfDive { get; }
    public Vector2 forwardDirection = Vector2.up;
    public Sprite TookPosition;
    public Sprite PikePosition;
    public Sprite EntrancePosition;
    public float RotateDirection { get; private set; }

    public ClassOfDive(
        SpriteCollection collection,
        DiveType SelectedClassOfDive,
        Transform transform,
        BoxCollider2D BoxCollider, 
        SpriteRenderer spriteRenderer)
    {
        this.SelectedClassOfDive = SelectedClassOfDive;
        SetJumpForce();
        SetForwardDirection();
        var StandSprite = collection.StandSprites[SelectedClassOfDive];
        ChangeSpriteInStand(StandSprite, transform, BoxCollider, spriteRenderer);
        ChoosePositionSprites(collection);
        ChooseRotateDirection();
    }

    public ClassOfDive(DiveType SelectedClassOfDive)
    {
        this.SelectedClassOfDive = SelectedClassOfDive;
    }

    private void SetJumpForce()
    {
        if (SelectedClassOfDive is DiveType.SixthFrontClass || SelectedClassOfDive is DiveType.SixthBackClass)
            JumpForce = Constants.JumpForceFromArmStand;
        else 
            JumpForce = Constants.DefoltJumpForce;
    }

    private void SetForwardDirection()
    {
        if (JumpForce == Constants.JumpForceFromArmStand)
            forwardDirection = Vector2.down;
    }
    
    private void ChangeSpriteInStand(Sprite sprite, Transform transform, BoxCollider2D BoxCollider,
        SpriteRenderer spriteRenderer)
    {
        BoxCollider.size = new Vector2(BoxCollider.size.x, SetColliderYSize());
        transform.position = new Vector2(transform.position.x + Constants.DeltaXInChangeStand,
            transform.position.y + SetDeltaY());
        spriteRenderer.sprite = sprite;
    }

    private float SetColliderYSize()
    {
        if (SelectedClassOfDive is DiveType.SecondClass || SelectedClassOfDive is DiveType.ThirdClass)
            return Constants.DefoltColliderYSize;
        else
            return Constants.ColliderYSizeInStandWirhArmsExtended;
    }

    private float SetDeltaY()
    {
        if (SelectedClassOfDive is DiveType.FirstClass || SelectedClassOfDive is DiveType.FourthClass)
            return Constants.NoDeltaHeight;
        else
            return Constants.DeltaHeightInOtherStands;
    }

    private void ChoosePositionSprites(SpriteCollection collection)
    {
        TookPosition = collection.PositionSprites[SelectedClassOfDive][DivePosition.Took];
        PikePosition = collection.PositionSprites[SelectedClassOfDive][DivePosition.Pike];
        EntrancePosition = collection.PositionSprites[SelectedClassOfDive][DivePosition.Entrance];
    }

    private void ChooseRotateDirection()
    {
        if (SelectedClassOfDive is DiveType.ThirdClass || SelectedClassOfDive is DiveType.FourthClass)
            RotateDirection = Constants.CounterClockwiseDirection;
        else 
            RotateDirection = Constants.ClockwiseDirection;
    }
}