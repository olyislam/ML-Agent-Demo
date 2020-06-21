using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class Player : Agent
{
    private Rigidbody rb;
    private Vector3 Initial_podition;
    private Quaternion Initial_rotation;

    public bool isDetected;
    public int speed;



    private void Update()
    {
        if (isDetected)
            RequestDecision();
         
    }
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        Initial_podition = transform.position;
        Initial_rotation = transform.rotation;

    }

   
    public override void Heuristic(float[] actionsOut)
    {
            actionsOut[0] = 0;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            actionsOut[0] = 1;
        }
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float received = vectorAction[0];
        Debug.Log("<color=green> received = </color>" + received);
        if (Mathf.FloorToInt(received) == 1)
        {
            isDetected = false;
            Jump();
        }
    }
    public override void OnEpisodeBegin()
    {
        reset();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Gnd")
        {
            isDetected = true;
        }
    }



    private void Jump()
    {
        rb.AddForce(Vector3.up * speed);
    }


    private void reset()
    {
        transform.position = Initial_podition;
        transform.rotation = Initial_rotation;
    }
}
