using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    [Header("Chase Settings")]
    public Transform player;
    public float moveSpeed;

    [Header("Light Settings")]
    public GameObject torch;

    // Update is called once per frame
    void Update()
    {
        // get direction to the player
        Vector3 moveDir = player.position - transform.position;
        moveDir.y = 0f;
    
        // get angle + dist to torch
        float angle = Vector3.Angle(torch.transform.forward, transform.position - torch.transform.position);
        float distance = (transform.position - torch.transform.position).magnitude;

        if(torch.GetComponent<Light>().enabled && 
           angle <= torch.GetComponent<Light>().spotAngle && 
           distance <= torch.GetComponent<Light>().range){

                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        // move towards the player
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
        
        // lookat the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
