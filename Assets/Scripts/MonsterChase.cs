using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterChase : MonoBehaviour
{
    public Transform player;
    public GameObject torchBeam;

    NavMeshAgent agent;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.position;

        // check for flashlight
        
        Light light = torchBeam.GetComponent<Light>();
        Vector3 offset = transform.position - torchBeam.transform.position;
        
        // shined in its eyes
        if(Vector3.Angle(torchBeam.transform.forward, offset) <= light.spotAngle && offset.magnitude <= light.range && light.intensity > 0f){
            target = transform.position;
        }

        // close to player and torch on
        if(offset.magnitude <= 0.3f){
            target = transform.position;
            Debug.Log("Nomnom");
        }

        agent.SetDestination(target);
    }
}
