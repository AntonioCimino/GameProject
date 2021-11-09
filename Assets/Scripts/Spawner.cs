using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab = null;
    public GameObject prefab1 = null;
    public GameObject prefab2 = null;
    public Transform reset = null;


    private void Start()
    {
        Invoke("SpawnObstacle", 1.0f);
    }
    private void SpawnObstacle()
    {
        int n = Random.Range(0, 3);

        if(n == 0){
            GameObject go = Instantiate(prefab);
            go.transform.position = new Vector3(reset.position.x + 1, reset.position.y, reset.position.z);
            Invoke("SpawnObstacle", 5.0f);
        }

         if(n == 1){
            GameObject go = Instantiate(prefab1);
            go.transform.position = new Vector3(reset.position.x - 1, reset.position.y, reset.position.z);
            Invoke("SpawnObstacle", 5.0f);
         }

         if(n == 2){
            GameObject go = Instantiate(prefab2);
            go.transform.position = new Vector3(reset.position.x + 4, reset.position.y, reset.position.z);
            Invoke("SpawnObstacle", 5.0f);
         }
    }
}
