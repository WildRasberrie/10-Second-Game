using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
  public void RestartGame()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
