using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardsNPCMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    private List<Vector3> buildingsPositions = new List<Vector3>();
    public GameObject buildingsGameObject;
    private int randomBuildingIndex, previousBuildingIndex;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AddBuildingsToList();

        // Get a random index within the bounds of the list
        randomBuildingIndex = UnityEngine.Random.Range(0, buildingsPositions.Count);

        agent.destination = buildingsPositions[randomBuildingIndex];
    }

    private void AddBuildingsToList()
    {
        // Loop through all children of the GameObject
        for (int i = 0; i < buildingsGameObject.transform.childCount; i++)
        {
            Transform child = buildingsGameObject.transform.GetChild(i);
            // Add the child to the list
            buildingsPositions.Add(child.position);
        }

    }

    void SetRandomDestination()
    {
        previousBuildingIndex = randomBuildingIndex;
        randomBuildingIndex = UnityEngine.Random.Range(0, buildingsPositions.Count);
        while (previousBuildingIndex == randomBuildingIndex)
        {
            randomBuildingIndex = UnityEngine.Random.Range(0, buildingsPositions.Count);
        }
        agent.destination = buildingsPositions[randomBuildingIndex];
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Building"))
        {
            SetRandomDestination();
        }
    }
}
