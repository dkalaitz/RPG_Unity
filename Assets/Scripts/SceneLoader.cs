using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] TMP_Text textIntroduction;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return) && textIntroduction.GetComponent<TypewriterEffect>().textWritten)
        {
            LoadNextScene();
        }
    }
    public void LoadNextScene()
    {
        // Load the next scene by index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
