using Unity.MLAgents;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MLAgentPlayer : Agent
{
    public float force = 20.0f;
    public Transform reset = null;
    public TextMesh score = null;
    public Vector3 jump;
    public Vector3 right;
    public Vector3 left;
    private Rigidbody rb = null;
    public bool air = true;
    private float points = 0;
    public float moveSpeed = 0.1f;
    public Button yourButton = null;

    public override void Initialize()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        rb = this.GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.0f, 0.0f);
        right = new Vector3(0.15f, 0.0f, 0.0f);
        left = new Vector3(-0.15f, 0.0f, 0.0f);
        ResetMyAgent();
    }

    void TaskOnClick(){
         points = 0;
         score.text = "Score: " + points.ToString();
	}

    public override void OnActionReceived(float[] vectorAction)
    {
        if (vectorAction[0] == 1)
        {
            UpForce();
        }

        if (vectorAction[0] == 2)
        {
            MoveRight();
        }

        if (vectorAction[0] == 3)
        {
            MoveLeft();
        }
    }

    public override void OnEpisodeBegin()
    {
        ResetMyAgent();
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.UpArrow) == true)
            actionsOut[0] = 1;
        if (Input.GetKey(KeyCode.RightArrow) == true)
            actionsOut[0] = 2;
        if (Input.GetKey(KeyCode.LeftArrow) == true)
            actionsOut[0] = 3;
    }

    private void OnCollisionStay(Collision collision)
    {

        //Premio per le ciliegie
        if (collision.gameObject.CompareTag("cherry") == true)
        {
            AddReward(0.5f);
            points = points + 2;
            score.text = "Score: " + points.ToString();
            Destroy(collision.gameObject);
        }

        //Penalità ostacoli
        if (collision.gameObject.CompareTag("obstacle") == true || collision.gameObject.CompareTag("obstacle1") == true
        || collision.gameObject.CompareTag("obstacle2") == true)
        {
            AddReward(-1.0f);
            Destroy(collision.gameObject);
            points = points - 1;
            score.text = "Score: " + points.ToString();
            EndEpisode();
        }

        //Penalità muri laterali e superiori
        if (collision.gameObject.CompareTag("walltop") == true)
        {
            //AddReward(-0.9f);
            //EndEpisode();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Premio superamento ostacoli laterali
        if (other.CompareTag("wallward1") == true || other.CompareTag("wallward2") == true)
        {
            AddReward(0.1f);
            points++;
            score.text = "Score: " + points.ToString();
        }

        //Premio superamento ostacoli inferiori
        if (other.CompareTag("wallward") == true)
        {
            AddReward(0.5f);
            points++;
            score.text = "Score: " + points.ToString();
        }
    }

    private void UpForce()
    {
        if(this.transform.position.y < 3f){
            air = true;
        }

        if(air == true){
            rb.velocity += jump;
            //Penalità salto
            AddReward(-0.005f);
            air = false;
        }
    }

    private void MoveRight()
    {
        rb.AddForce(right * moveSpeed, ForceMode.Impulse);
    }

    private void MoveLeft()
    {
        rb.AddForce(left * moveSpeed, ForceMode.Impulse);
    }

    private void ResetMyAgent()
    {
        this.transform.position = new Vector3(reset.position.x, reset.position.y - 1, reset.position.z);
    }
}
