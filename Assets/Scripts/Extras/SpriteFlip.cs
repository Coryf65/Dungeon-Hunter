using UnityEngine;

public class SpriteFlip : CharacterComponent
{
    public enum FlipMode
    {
        MovementDirection,
        WeaponDirection
    }

    [SerializeField] private FlipMode _flipMode = FlipMode.MovementDirection;
    [SerializeField] private float _threshold = 0.1f;

    public bool FacingRight { get; set; }

    void Awake()
    {
        FacingRight = true;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();

        if (_flipMode == FlipMode.MovementDirection)
        {
            FlipToMovementDirection();
        }

        if (_flipMode == FlipMode.WeaponDirection)
        {
            FlipToWeaponDirection();
        }
    }

    private void FlipToMovementDirection()
    {
        // we are moving
        if (controller.CurrentMovement.normalized.magnitude > _threshold)
        {
            if (controller.CurrentMovement.normalized.x > 0)
            {
                FaceDirection(1);
            }
            else
            {
                FaceDirection(-1);
            }
        }
    }

    /// <summary>
    /// Flips our character by our weapon aiming
    /// </summary>
    private void FlipToWeaponDirection()
    {
        // 
        if (characterWeapon != null)
        {
            float weaponAngle = characterWeapon.WeaponAim.CurrentAimAngleAbsolute;
            if (weaponAngle > 90 || weaponAngle < -90)
            {
                FaceDirection(-1); // left
            }
            else
            {
                FaceDirection(1); // right
            }
        }
    }

    /// <summary>
    /// Makes our character face the direction of movement
    /// </summary>
    /// <param name="newDirection">direction to face</param>
    private void FaceDirection(int newDirection)
    {
        // moving right
        if (newDirection == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            FacingRight = true;
        }

        if (newDirection <= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            FacingRight = false;
        }
    }

}