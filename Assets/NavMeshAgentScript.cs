using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentScript : MonoBehaviour
{
    private GameObject obj;

    private void Start()
    {
        obj = this.gameObject;
    }

    // Function to find the nearest NavMesh surface to a given point
    public NavMeshSurface FindNearestNavMeshSurface(Vector3 point)
    {
        NavMeshSurface[] navMeshSurfaces = FindObjectsOfType<NavMeshSurface>(); // Find all NavMesh surfaces in the scene
        NavMeshSurface nearestSurface = null;
        float shortestDistance = float.MaxValue;

        // Iterate through all NavMesh surfaces to find the nearest one
        foreach (NavMeshSurface surface in navMeshSurfaces)
        {
            Vector3 surfaceCenter = surface.transform.position + surface.center;
            float distance = Vector3.Distance(point, surfaceCenter);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestSurface = surface;
            }
        }

        return nearestSurface;
    }

    // Function to place an object on the nearest NavMesh surface
    public void PlaceOnNearestNavMeshSurface()
    {
        // Get the current position of the object
        Vector3 currentPosition = obj.transform.position;

        // Find the nearest NavMesh surface to the current position
        NavMeshSurface nearestSurface = FindNearestNavMeshSurface(currentPosition);
        if (nearestSurface != null)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(currentPosition, out hit, 5f, NavMesh.AllAreas))
            {
                obj.transform.position = hit.position;
                Debug.Log("Object placed on the nearest NavMesh surface: " + nearestSurface.name);
            }
            else
            {
                Debug.LogWarning("Failed to find a valid position on the nearest NavMesh surface!");
            }
        }
        else
        {
            Debug.LogWarning("No NavMesh surfaces found in the scene!");
        }
    }
}
