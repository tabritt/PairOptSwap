using UnityEngine;

public class BlockItem : MonoBehaviour
{
    private int hitCount = 0;
    private Vector3 originalScale;

    public float healthDropChance = 0.25f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnHit()
    {
        hitCount++;

        if (hitCount == 1)
        {
            ShrinkBlock();
        }
        else if (hitCount == 2)
        {
            DestroyBlock();
        }
    }

    void ShrinkBlock()
    {
        transform.localScale = originalScale * 0.5f;  // hit then small then destroy
    }

    void DestroyBlock()
    {
        if (Random.value <= healthDropChance)
        {
            DropHealthItem();
        }

        Destroy(gameObject);
    }

    void DropHealthItem()
    {
        GameObject healthItemPrefab = Resources.Load<GameObject>("HealthItem");  // Load from Resources
        if (healthItemPrefab != null)
        {
            Instantiate(healthItemPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Health item prefab not found!");
        }
    }
}