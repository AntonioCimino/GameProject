using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject prefab = null;
    public GameObject prefab1 = null;
    public GameObject prefab2 = null;
    public GameObject c = null;
    public Transform reset = null;
    public GameObject EndGame = null;
    public float timeRemaining = 10;
    public Button yourButton = null;

    private void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        Invoke("SpawnObstacle", 1.0f);
    }

    void TaskOnClick(){
        timeRemaining = 10;
        EndGame.gameObject.SetActive(false);
        Invoke("SpawnObstacle", 1.0f);
	}

    private void SpawnObstacle()
    {
        if (timeRemaining > 0)
        {
            //timeRemaining = timeRemaining - 1;


            int n = Random.Range(0, 3);
            int charry = Random.Range(0, 3);
            int p = Random.Range(0, 2);

            if(n == 0){
                GameObject go = Instantiate(prefab);
                go.transform.position = new Vector3(reset.position.x + 1, reset.position.y - 4.5f, reset.position.z);
                if(charry == 2){
                    go = Instantiate(c);
                    go.transform.position = new Vector3(reset.position.x - 1 + (p*4f), reset.position.y, reset.position.z);
                }
                Invoke("SpawnObstacle", 5.0f);
            }

            if(n == 1){
                GameObject go = Instantiate(prefab1);
                go.transform.position = new Vector3(reset.position.x - 1, reset.position.y, reset.position.z);
                if(charry == 2){
                    go = Instantiate(c);
                    go.transform.position = new Vector3(reset.position.x - 1 + (p*4f), reset.position.y - 5, reset.position.z - 9);
                }
                Invoke("SpawnObstacle", 5.0f);
            }

            if(n == 2){
                GameObject go = Instantiate(prefab2);
                go.transform.position = new Vector3(reset.position.x + 4, reset.position.y, reset.position.z);
                if(charry == 2){
                    go = Instantiate(c);
                    go.transform.position = new Vector3(reset.position.x - 1 + (p*4f), reset.position.y - 5, reset.position.z - 10);
                }
                Invoke("SpawnObstacle", 5.0f);
            }
        } else {
            EndGame.gameObject.SetActive(true);
        }
    }
}
