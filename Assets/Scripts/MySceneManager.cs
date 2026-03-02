using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    //public‚Í’†•Manager‚æ‚èŒÄ‚Ño‚µ‚æ‚èŒÄ‚Ño‚µ
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }




}
