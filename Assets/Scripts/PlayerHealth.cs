using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float fireDamage = 10f;
    [SerializeField]
    private float burnReactivationTime = 0.5f;

    public bool isCheckingBurn = false;

    // Update is called once per frame
    void Update()
    {
        if (!isCheckingBurn)
        {
            if (IsTileOnFire())
            {
                StartCoroutine(burn());
            }
        }
        death();
    }

    private bool IsTileOnFire()
    {
        int layerMask = 1 << 9; // layer of map tile

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3, layerMask))
        {
            TileFire tileBelow = hit.collider.gameObject.GetComponentInParent<TileFire>();
            if (tileBelow != null)   //Make sure we are above tile
            {
                Debug.Log("Tile below");
                if(tileBelow.fireResistanceCurrent <= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void death()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator burn()
    {
        isCheckingBurn = true;
        health -= fireDamage;
        yield return new WaitForSeconds(burnReactivationTime);
        isCheckingBurn = false;
        yield return null;
    }
}
