using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public string sceneA = "FirstLevel";
    public string sceneB = "SecondLevel";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string targetScene = currentScene == sceneA ? sceneB : sceneA;

            SceneManager.LoadScene(targetScene);
        }
    }
}
