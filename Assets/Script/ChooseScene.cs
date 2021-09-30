using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseScene : MonoBehaviour
{
    public void SceneAI()
    {
        SceneManager.LoadScene(2);
    }

    public void Scene1v1()
    {
        SceneManager.LoadScene(1);
    }
}
