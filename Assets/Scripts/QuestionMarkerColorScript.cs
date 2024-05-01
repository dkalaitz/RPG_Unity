using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkerColorScript : MonoBehaviour
{

    public Color color = Color.yellow; // Set the desired color in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = color;
    }


}
