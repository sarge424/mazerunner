using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterChase : MonoBehaviour
{
    public Transform player;
    public GameObject torchBeam;
    public Transform lightSensor;

    NavMeshAgent agent;
    Animator anim;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.position;

        // check for flashlight
        
        Light light = torchBeam.GetComponent<Light>();
        Vector3 offset = lightSensor.position - torchBeam.transform.position;
        
        // shined in its eyes
        if(Vector3.Angle(torchBeam.transform.forward, offset) <= light.spotAngle && offset.magnitude <= light.range && light.intensity > 0f){
            target = transform.position;
            anim.speed = 0;
        }else{
            anim.speed = 2;
        }

        Debug.Log("Offset" + offset.magnitude);

        agent.SetDestination(target);
    }
}
