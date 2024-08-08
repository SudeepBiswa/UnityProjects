using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Array of item prefabs to be spawned
    public GameObject[] itemPrefabs;

    // Define the boundaries of the base floor
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    // Height at which the items should be spawned
    public float spawnHeight = 1f;

    // Minimum distance between spawned items
    public float minDistanceBetweenItems = 2f;

    // Maximum attempts to find a valid position
    public int maxAttempts = 10;

    // Half-extents of the box used for overlap checks
    public Vector3 overlapBoxSize = new Vector3(1f, 1f, 1f);


    // Start is called before the first frame update
    void Start()
    {
        // Spawn the items
        SpawnItems();
    }

    // Method to spawn the items at random locations
    void SpawnItems()
    {
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            bool validPositionFound = false;
            int attempts = 0;

            while (!validPositionFound && attempts < maxAttempts)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ));

                // Check if the position is valid (no other objects within minDistanceBetweenItems)
                if (IsPositionValid(spawnPosition))
                {
                    Instantiate(itemPrefabs[i], spawnPosition, Quaternion.identity, transform);
                    validPositionFound = true;
                }

                attempts++;
            }

            if (!validPositionFound)
            {
                //if a valid position cannot be found then print message and spawn at random location.
                Debug.LogWarning($"Could not find a valid position to spawn {itemPrefabs[i].name} after {maxAttempts} attempts.");
                Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnHeight, Random.Range(minZ, maxZ));
                Instantiate(itemPrefabs[i], spawnPosition, Quaternion.identity, transform);
            }
        }
    }

    // Method to check if a position is valid
    bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapBox(position, overlapBoxSize / 2);
        foreach (Collider collider in colliders)
        {
            // Ensure the collider belongs to a spawned item and not something else in the scene
            if (collider.CompareTag("SpawnedItem"))
            {
                return false;
            }
        }
        return true;
    }
}
