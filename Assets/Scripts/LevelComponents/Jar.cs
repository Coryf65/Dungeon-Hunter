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
    private ComponentBase componentBase;

    private void Start()
    {
        componentBase = GetComponent<ComponentBase>();
        componentBase.OnJarBroken += SpawnLoot;
    }

    private void SpawnLoot(object sender, System.EventArgs e)
    {
        float probability = Random.Range(0, 100);
        //Debug.Log($"drop roll: {probability}");

        if (probability > _dropLootChancePercent)
        {
            _lootSpawnArea.x = Random.Range(-_randomXPosition, _randomXPosition);
            _lootSpawnArea.y = Random.Range(-_randomYPosition, _randomYPosition);

            Instantiate(original: GetRandomLoot(), position: transform.position + _lootSpawnArea, rotation: Quaternion.identity);

            componentBase.OnJarBroken -= SpawnLoot;
        }
    }

    private GameObject GetRandomLoot()
    {
        int randomIndex = Random.Range(0, _jarLoot.Length);

        return _jarLoot[randomIndex];
    }       
}