using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex;
    public GameObject spawnExit;

    private float _t = 0f;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }

        }
        if (popUpIndex >= 0 || popUpIndex < popUps.Length - 1)
        {
            _t += Time.deltaTime;
            if(_t >= 6f)
            {
                _t = 0f;
                popUpIndex++;
            }
        }
        else
            popUpIndex = 0;
        

    }


    private void nextTip()
    {
        print("started");
        popUpIndex += 1;
        return;
    }


    public void OnPlay ()
    {
        SceneManager.LoadScene(1);
    }

    public void OnPlayAgain()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene("next");
    }
    public void OnSceneSelect()
    {
        SceneManager.LoadScene("Course Select");
    }

    public void JumpCourse()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveCourse()
    {
        SceneManager.LoadScene(2);
    }

    public void FiringRange()
    {
        SceneManager.LoadScene(3);
    }

    public void Cover()
    {
        SceneManager.LoadScene(4);
    }

    public void Tracking()
    {
        SceneManager.LoadScene(5);
    }

    public void Corners()
    {
        SceneManager.LoadScene(6);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void DoExitGame()
    {
        Application.Quit();
    }
}
