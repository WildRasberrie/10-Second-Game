using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    Scene_Manager Scene_Manager;
        void Start()
        {
            Scene_Manager = FindFirstObjectByType<Scene_Manager>();
        }
  public void RestartGame()
    {
        Scene_Manager.StartGame();
    }
}
