using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent EnemyAgent;
    private Transform Player;

    void Awake(){
        Player = GameObject.Find("Player").transform;
        EnemyAgent = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        AgentMove();

    }
    void AgentMove(){
        EnemyAgent.SetDestination(Player.position);
    }



}
