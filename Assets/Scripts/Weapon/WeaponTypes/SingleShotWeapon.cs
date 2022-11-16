using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWeapon : Weapon
{
    [SerializeField] private Vector3 _projectileSpawnPoint;
    private Vector3 _projectileSpawnValue;

    public Vector3 ProjectileSpawnPoint { get; set; }
    public ObjectPooler Pooler { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        _projectileSpawnValue = _projectileSpawnPoint;
        _projectileSpawnValue.y = -_projectileSpawnPoint.y;

        Pooler = GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

    private void SpawnProjectile(Vector2 position)
    {
        bool facingDirection = false;
        
        GameObject projectilePooled = Pooler.GetObjectFromPool();
        projectilePooled.transform.position = position;
        projectilePooled.SetActive(true);

        Projectile projectile = projectilePooled.GetComponent<Projectile>();        
        Vector2 newDirection = WeaponUser.GetComponent<SpriteFlip>().FacingRight ? transform.right : transform.right * -1;

        if (newDirection.x > 0)
        {
            facingDirection = true;
        }       

        projectile.SetDirection(newDirection, transform.rotation, facingDirection);
        CanShoot = false;
    }

    private void OnDrawGizmosSelected()
    {
        // draw gizoms to see in editor
        CalculateProjectileSpawns();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center: ProjectileSpawnPoint, radius: 0.1f);
    }

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