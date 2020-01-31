using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] SceneReference scene;

    [Button("Changer de scene")]
    public void SetScene()
    {
        SceneManager.LoadScene(scene);
    }

}
