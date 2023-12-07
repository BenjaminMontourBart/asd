using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent Agent;
    private Animator Animation;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animation = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        float velocity = Agent.velocity.magnitude;
        if (Input.GetMouseButtonDown(0))
        {
            Move();
            Animation.SetBool("IfWalk", true);
        }
        else if (velocity == 0)
        {
            Animation.SetBool("IfWalk", false);
        }
        else
        {
            Animation.SetBool("IfWalk", true);
        }
    }
    private void Move()
    {
        Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHitInfo;
        if (Physics.Raycast(rayFromCamera, out raycastHitInfo))
        {
            if (raycastHitInfo.collider.CompareTag("Ennemi"))
            {
                Agent.stoppingDistance = 10;
                Agent.SetDestination(raycastHitInfo.collider.transform.position);
            }
            else
            {
                Agent.stoppingDistance = 0;
                Agent.SetDestination(raycastHitInfo.point);
            }
        }
    }
}
