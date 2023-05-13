using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private Transform lastSeen;
    private NavMeshAgent agent;

    public float attackRange = 5f;
    public float decisionRange = 1f;

    public float decisionTime;
    public float forgetTime = 8;

    bool lostSight = true;
    bool newDest = true;

    //0 patrolling, 1 chasing, 2 attacking, 3 fleeing
    int state = 0;

    public float patrolRange; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in




    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        // if(lastSeen == null)
        // {
        //     state = 0;
        // }
        // else
        // {
        //     state = 1;
        // }

        switch (state)
        {
            case 0:
            {
                Patrolling();
                break;
            }
            case 1:
            {
                Chasing();
                break;
            }
            case 2:
            {
                Attacking();
                break;
            }
            default:
                break;
        }
    }

    void Patrolling()
    {
        if(newDest)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, patrolRange, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
                newDest = false;

                Invoke("DestReset", decisionTime);
            }
        }
    }

    void DestReset()
    {
        newDest = true;
    }

    void Chasing()
    {
        if(lostSight)
        {
            StartCoroutine(StartWait());

            transform.LookAt(lastSeen.position);
            agent.SetDestination(lastSeen.position);
        }
        else
        {
            transform.LookAt(lastSeen.position);
            agent.SetDestination(lastSeen.position);

            float dist = Vector3.Distance(lastSeen.position, transform.position);

            if(dist < attackRange)
            {
                state = 2;
            }
        }
    }

    void Attacking()
    {
        Debug.Log("attacking");
        state = 1;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            lostSight = false;
            lastSeen = other.gameObject.transform;

            if(state != 2)
            {
                state = 1;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        lostSight = true;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;

        for (int i = 0; i < 30; i++)
        {
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            { 
                //the 1.0f is the max distance from the random point to a point on the navmesh
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    IEnumerator StartWait()
    {
        yield return new WaitForSeconds(forgetTime);
        if(lostSight)
        {
            state = 0;
            lastSeen = null;
        }
        else
        {
            state = 1;
        }
    }
}
