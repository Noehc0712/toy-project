using UnityEngine;

public class TargetMaker : MonoBehaviour
{
    public GameObject target;
    private float spawnRangeX = 80;
    private float spawnRangeY = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(15, spawnRangeY), 50);

            Instantiate(target, spawnPos, target.transform.rotation);
        }
    }
}