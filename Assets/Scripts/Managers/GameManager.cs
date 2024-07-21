using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended && _isGameOver == true)
            {
                SceneManager.LoadScene(0); // 0 means Current Game Scene.
            }
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
