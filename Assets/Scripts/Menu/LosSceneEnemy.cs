using Maze;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MazeMenu
{

    public class LosSceneEnemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private AudioSource _audsStep;
        [SerializeField] private Transform[] waypoints;

        int m_CurrentWaypointIndex;

        private void Awake()
        {            
            navMeshAgent.SetDestination(waypoints[0].position);
        }

        void Update()
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }

    }
}
