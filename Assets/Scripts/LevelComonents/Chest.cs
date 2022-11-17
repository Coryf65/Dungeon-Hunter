using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Chest Loot Settings")]
    [SerializeField] private GameObject[] _chestLoot;

    [Header("Loot Drop Area")]
    [SerializeField] private float _randomXPosition = 2f;
    [SerializeField] private float _randomYPosition = 2f;

    private bool _canSpawnLoot = false;
    private bool _rewardSpawned = false;
    private Vector3 _lootSpawnArea = Vector3.zero;
    private readonly int _chestOpenedTrigger = Animator.StringToHash("ChestOpened");
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_canSpawnLoot)
            {
                SpawnLoot();
            }
        }
    }

    private void SpawnLoot()
    {
        if (_canSpawnLoot && !_rewardSpawned)
        {
            _animator.SetTrigger(_chestOpenedTrigger);            
            _lootSpawnArea.x = Random.Range(-_randomXPosition, _randomXPosition);
            _lootSpawnArea.y = Random.Range(-_randomYPosition, _randomYPosition);
            Instantiate(original: GetRandomLoot(), position: transform.position + _lootSpawnArea, rotation: Quaternion.identity);
            _rewardSpawned = true;
        }
    }

    /// <summary>
    /// Get a Random Lootable GameObject from our available Loot pool
    /// </summary>
    /// <returns></returns>
    private GameObject GetRandomLoot()
    {
        int randomIndex = Random.Range(0, _chestLoot.Length);

        return _chestLoot[randomIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canSpawnLoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canSpawnLoot = false;
        }
    }
}