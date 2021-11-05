using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab = null;
    public GameObject prefab1 = null;
    public GameObject prefab2 = null;

    private void Start()
    {
        Invoke("SpawnObstacle", 1.0f);
    }
    private void SpawnObstacle()
    {
        int n = Random.Range(0, 3);

        if(n == 0){
            GameObject go = Instantiate(prefab);
            go.transform.position = new Vector3(-8.60f, 1.50f, -7.50f);
            Invoke("SpawnObstacle", 5.0f);
        }

         if(n == 1){
            GameObject go = Instantiate(prefab1);
            go.transform.position = new Vector3(-10.60f, 7.50f, -1f);
            Invoke("SpawnObstacle", 5.0f);
         }

         if(n == 2){
            GameObject go = Instantiate(prefab2);
            go.transform.position = new Vector3(-6.60f, 7.50f, 1f);
            Invoke("SpawnObstacle", 5.0f);
         }
    }
}
