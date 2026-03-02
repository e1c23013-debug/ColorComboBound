using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    //publicは中枢Managerより呼び出しより呼び出し
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }




}
