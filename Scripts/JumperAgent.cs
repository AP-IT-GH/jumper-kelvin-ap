using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class JumperAgent : Agent
{
    public float force = 30f;
    public Transform reset = null;

    private Rigidbody rb = null;
    private float elapsedTime = 0f;
    private const float REWARD_PER_SECOND = 0.1f;

    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        ResetMyAgent();
    }

    private void ResetMyAgent()
    {
        this.transform.position = new Vector3(reset.position.x, reset.position.y, reset.position.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float upForce = actions.ContinuousActions[0];

        if (upForce > 0.5f)
        {
            UpForce();
        }
        else if (upForce < -0.5f)
        {
            DownForce();
        }

        AddReward(REWARD_PER_SECOND * Time.timeScale);

        if (elapsedTime >= 1f)
        {
            AddReward(REWARD_PER_SECOND);
            elapsedTime -= 1f;
        }

    }

    public override void OnEpisodeBegin()
    {
        ResetMyAgent();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if (Input.GetKey(KeyCode.UpArrow))
            continuousActionsOut[0] = 1.0f;
        else if (Input.GetKey(KeyCode.DownArrow))
            continuousActionsOut[0] = -1.0f;
        else
            continuousActionsOut[0] = 0.0f;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            AddReward(-1.5f);
            Destroy(collision.gameObject);
            EndEpisode();
            ResetMyAgent();
        }
        if (collision.gameObject.CompareTag("wallTop"))
        {
            AddReward(-0.2f);
            EndEpisode();
            ResetMyAgent();
        }
        if (collision.gameObject.CompareTag("road"))
        {
            AddReward(-0.2f);
            EndEpisode();
            ResetMyAgent();
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void UpForce()
    {
        rb.AddForce(Vector3.up * force * 2, ForceMode.Acceleration);
    }

    private void DownForce()
    {
        rb.AddForce(Vector3.down * force, ForceMode.Acceleration);
    }
}
