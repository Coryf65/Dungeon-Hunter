using UnityEngine;

public class SingleShotWeapon : Weapon
{
    [SerializeField] private Vector3 _projectileSpawnPoint;
    [SerializeField] private Vector3 _projectileSpread;
    private Vector3 _projectileSpawnValue;
    private Vector3 _randomProjectileSpread;

    /// <summary>
    /// Controls the position of our projectile spawn
    /// </summary>
    public Vector3 ProjectileSpawnPoint { get; set; }
    /// <summary>
    /// Return the reference to the pooler in this gameobject
    /// </summary>
    public ObjectPooler Pooler { get; set; }

    protected override void Awake()
    {
        base.Awake();
        _projectileSpawnValue = _projectileSpawnPoint;
        _projectileSpawnValue.y = -_projectileSpawnPoint.y;

        Pooler = GetComponent<ObjectPooler>();
    }

    protected override void HandleShooting()
    {
        base.HandleShooting();

        if (CanShoot)
        {
            CalculateProjectileSpawns();
            SpawnProjectile(ProjectileSpawnPoint);
        }
    }

    /// <summary>
    /// Spawns a projecile from the pool setting it's new direction based on the characters direction (WeaponUser)
    /// </summary>
    /// <param name="position">where the projectile gets firedS</param>
    private void SpawnProjectile(Vector2 position)
    {
        bool facingDirection = false;

        // Get an object from the pool
        GameObject projectilePooled = Pooler.GetObjectFromPool();
        projectilePooled.transform.position = position;
        projectilePooled.SetActive(true);

        // get a refernce to the projectile
        Projectile projectile = projectilePooled.GetComponent<Projectile>();

        // spread amount for weapon
        _randomProjectileSpread.z = Random.Range(minInclusive: -_projectileSpread.z, maxInclusive: _projectileSpread.z);
        Quaternion spread = Quaternion.Euler(_randomProjectileSpread);

        // set direction and rotation
        Vector2 newDirection = WeaponUser.GetComponent<SpriteFlip>().FacingRight ? spread * transform.right : spread * transform.right * -1;

        if (newDirection.x > 0)
        {
            facingDirection = true;
        }

        projectile.SetDirection(newDirection, transform.rotation, facingDirection);
        // Diable shot after next shot time
        CanShoot = false;
    }

    private void OnDrawGizmosSelected()
    {
        // draw gizoms to see in editor
        CalculateProjectileSpawns();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center: ProjectileSpawnPoint, radius: 0.1f);
    }

    /// <summary>
    /// Calculates the position where our projectile is going to be fired
    /// </summary>
    private void CalculateProjectileSpawns()
    {
        if (WeaponUser.GetComponent<SpriteFlip>().FacingRight)
        {
            ProjectileSpawnPoint = transform.position + transform.rotation * _projectileSpawnPoint;
        }
        else
        {
            ProjectileSpawnPoint = transform.position - transform.rotation * _projectileSpawnValue;
        }


    }
}