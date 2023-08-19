using System.Linq;
using UnityEngine;

public class EnemiesPoolControl : MonoBehaviour
{
    GameObject pool;

    void Start()
    {
        pool = GameObject.FindGameObjectWithTag("EnemiesPool");
    }

    public void SetPool()
    {
        var newPool = Instantiate(pool, pool.transform.position, Quaternion.identity);
        Destroy(pool);
        pool = newPool;
    }
}
