using UnityEngine;

public class Jar : MonoBehaviour
{
    [Header("Jar Loot Settings")]
    [SerializeField] private GameObject[] _jarLoot;

    [Header("Loot Drop Area")]
    [SerializeField] private float _randomXPosition = 2f;
    [SerializeField] private float _randomYPosition = 2f;
    [Range(0, 100)]
    [SerializeField] private float _dropLootChancePercent = 50f;

    private Vector3 _lootSpawnArea = Vector3.zero;

    private void SpawnLoot()
    {
        float probability = Random.Range(0, 100);
        Debug.Log($"drop roll: {probability}");

        if (probability > _dropLootChancePercent)
        {
            _lootSpawnArea.x = Random.Range(-_randomXPosition, _randomXPosition);
            _lootSpawnArea.y = Random.Range(-_randomYPosition, _randomYPosition);

            Debug.Log("Instantiate loot");
            Instantiate(original: GetRandomLoot(), position: transform.position + _lootSpawnArea, rotation: Quaternion.identity);
        }
    }

    private GameObject GetRandomLoot()
    {
        int randomIndex = Random.Range(0, _jarLoot.Length);

        return _jarLoot[randomIndex];
    }

    private void OnDisable()
    {
        // not calling check out
        Debug.Log("OnDisable()");
        SpawnLoot();
    }
}