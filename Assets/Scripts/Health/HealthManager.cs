using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager
{
    public float health;
    private float fireDamage;
    private float burnReactivationTime;

    private bool isCheckingBurn = false;

    public HealthManager(float health, float fireDamage, float burnReactivationTime)
    {
        this.health = health;
        this.fireDamage = fireDamage;
        this.burnReactivationTime = burnReactivationTime;
    }

    public bool IsAllowedToBurn(Vector3 objectPosition)
    {
        return !isCheckingBurn && IsTileOnFire(objectPosition);
    }

    private bool IsTileOnFire(Vector3 position)
    {
        int layerMask = 1 << 9; // layer of map tile

        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 3, layerMask))
        {
            TileFire tileBelow = hit.collider.gameObject.GetComponentInParent<TileFire>();
            if (tileBelow != null)   //Make sure we are above tile
            {
                if (tileBelow.fireResistanceCurrent <= 0 && tileBelow.fireDuration > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsDead
    {
        get => health <= 0;
    }

    public IEnumerator Burn()
    {
        isCheckingBurn = true;
        health -= fireDamage;
        yield return new WaitForSeconds(burnReactivationTime);
        isCheckingBurn = false;
        yield return null;
    }
}
