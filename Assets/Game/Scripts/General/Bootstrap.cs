
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        GameController.CreateIfNeeded();
    }

    private void Start()
    {
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene("Loading");
    }
}
