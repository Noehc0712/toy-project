using UnityEngine;

public class DestroyTarget : MonoBehaviour
{
    void Start()
    {
        Invoke("DeletObject", 5);
    }
    void Update()
    {

    }
    void DeletObject()
    {
        Destroy(gameObject);
    }
}
