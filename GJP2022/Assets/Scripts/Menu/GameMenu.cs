using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float timeStartGame;
    public void StartGame(int index)
    {
        StartCoroutine(LoadGame(index));
    }

    IEnumerator LoadGame(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(timeStartGame);
        SceneManager.LoadScene(index);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
