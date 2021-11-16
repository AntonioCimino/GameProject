using Unity.MLAgents;
using UnityEngine;

public class MLAgentPlayer : Agent
{
    public float force = 50.0f;
    public Transform reset = null;
    public TextMesh score = null;
    public Vector3 jump;
    public Vector3 right;
    public Vector3 left;
    private Rigidbody rb = null;
    public bool air = true;
    private float points = 0;
    public float moveSpeed = 0.1f;
    
    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 0.15f, 0.0f);
        right = new Vector3(0.15f, 0.0f, 0.0f);
        left = new Vector3(-0.15f, 0.0f, 0.0f);
        ResetMyAgent();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (vectorAction[0] == 3)
        {
            UpForce();
        }

        if (vectorAction[0] == 1)
        {
            MoveRight();
        }

        if (vectorAction[0] == 2)
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
        //actionsOut[1] = 0;
        if (Input.GetKey(KeyCode.UpArrow) == true)
            actionsOut[0] = 3;
        if (Input.GetKey(KeyCode.RightArrow) == true)
            actionsOut[0] = 1;
        if (Input.GetKey(KeyCode.LeftArrow) == true)
            actionsOut[0] = 2;
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("cherry") == true)
        {
            AddReward(0.4f);
            points = points + 2;
            score.text = "Score: " + points.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("obstacle") == true || collision.gameObject.CompareTag("obstacle1") == true
        || collision.gameObject.CompareTag("obstacle2") == true)
        {
            AddReward(-0.8f);          
            Destroy(collision.gameObject);
            EndEpisode();
        }

        if (collision.gameObject.CompareTag("walltop") == true)
        {
            AddReward(-0.8f);
            EndEpisode();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wallward") == true || other.CompareTag("wallward1") == true || other.CompareTag("wallward2") == true)
        {
            AddReward(0.1f);
            points++;
            score.text = "Score: " + points.ToString();
        }     
    }

    private void UpForce()
    {
        if(this.transform.position.y < 2f){
            air = true;
        }

        if(air == true){
            rb.AddForce(jump * force, ForceMode.Impulse);
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
        this.transform.position = new Vector3(reset.position.x, reset.position.y, reset.position.z);
    }
}
