using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWeapon : Weapon
{
    [SerializeField] private Vector3 _projectileSpawnPoint;
    private Vector3 _projectileSpawnValue;

    public Vector3 ProjectileSpawnPoint { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _projectileSpawnValue = _projectileSpawnPoint;
        _projectileSpawnValue.y = -_projectileSpawnPoint.y;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void HandleShooting()
    {
        base.HandleShooting();
        //Fire

    }

    private void SpawnProjectile(Vector2 position)
    {

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