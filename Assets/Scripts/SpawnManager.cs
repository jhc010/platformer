using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public int maxPlatforms = 20;
    public GameObject platform;

    public float horiontalMin = 6.5f;
    public float horizontalMax = 14f;
    public float verticalMin = -6f;
    public float verticalMax = 3f;

    private Vector2 prevPlatformPos;

	void Start () {
        prevPlatformPos = transform.position;
        Spawn();
	}
	
    void Spawn()
    {
        for (int i = 0; i < maxPlatforms; i++)
        {
            Vector2 randomSpawnPos = prevPlatformPos + new Vector2(
                Random.Range(horiontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
            Instantiate(platform, randomSpawnPos, Quaternion.identity);
            prevPlatformPos = randomSpawnPos;
        }
    }
}
