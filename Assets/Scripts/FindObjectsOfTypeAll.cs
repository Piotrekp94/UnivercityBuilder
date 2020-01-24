using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindObjectsOfTypeAll<T> : MonoBehaviour
{

    public static List<T> findObject()
    {
        List<T> results = new List<T>();
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            var s = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                var allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++)
                {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }
        return results;

    }
}
