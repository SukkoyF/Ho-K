using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{ 
    public void Load(string toLoad)
    {
        SceneManager.LoadScene(toLoad);
    }
}
