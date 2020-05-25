using UnityEngine;

public abstract class SingletonPrefabBehaviour<T> 
    : MonoBehaviour where T : SingletonPrefabBehaviour<T> {
    private static T _instance;

    public static T Instance {
        get {
            if (_instance != null) return _instance;
            
            _instance = FindObjectOfType<T>();
            if (_instance != null) return _instance;
            
            var prefab = Resources.Load("Prefabs/" + typeof(T));
            if (!prefab)
            {
                Debug.LogError("Prefab " + typeof(T) + " does not exist");
                return null;
            }
            
            Debug.Log("Spawning new PrefabSingleton" + typeof(T));

            var newGameObject = Instantiate(prefab as GameObject);
            _instance = newGameObject.GetComponent<T>();
            
            return _instance;
        }
    }
}