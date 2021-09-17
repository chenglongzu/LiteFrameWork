using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace LiteFramework
{
    public class SceneLoadManager : BaseManager<SceneLoadManager>
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadSceneAsync(string scneneName, UnityAction unityAction, UnityAction<float> process = null)
        {
            MonoManager.GetInstance().StartCoroutine(ReallyLoadSceneAsync(scneneName, unityAction, process));
        }
        private IEnumerator ReallyLoadSceneAsync(string scneneName, UnityAction unityAction, UnityAction<float> process)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(scneneName);
            while (!ao.isDone)
            {
                if (process != null)
                {
                    process(ao.progress);
                }

                yield return ao.progress;
            }

            if (ao.isDone)
            {
                unityAction();
            }

        }
    }
}
