using UnityEngine;

public class TargetMaker : MonoBehaviour
{
    public GameObject target;
    private float spawnRangeX = 80;
    private float spawnRangeY = 30;
    private float delay = 3;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnTarget", delay, delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnTarget()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(15, spawnRangeY), 74.9f);
        Instantiate(target, spawnPos, target.transform.rotation);
    }
}