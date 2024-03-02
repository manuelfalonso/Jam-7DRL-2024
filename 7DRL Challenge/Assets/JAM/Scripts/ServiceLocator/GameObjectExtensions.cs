using UnityEngine;

public static class GameObjectExtensions {
    /// <summary>
    /// Gets a component of the given type attached to the GameObject. If that type of component does not exist, it adds one.
    /// </summary>
    /// <remarks>
    /// This method is useful when you don't know if a GameObject has a specific type of component,
    /// but you want to work with that component regardless. Instead of checking and adding the component manually,
    /// you can use this method to do both operations in one line.
    /// </remarks>
    /// <typeparam name="T">The type of the component to get or add.</typeparam>
    /// <param name="gameObject">The GameObject to get the component from or add the component to.</param>
    /// <returns>The existing component of the given type, or a new one if no such component exists.</returns>    
    public static T GetOrAdd<T> (this GameObject gameObject) where T : Component {
        T component = gameObject.GetComponent<T>();
        if (!component) component = gameObject.AddComponent<T>();
        
        return component;
    }

    /// <summary>
    /// Returns the object itself if it exists, null otherwise.
    /// </summary>
    /// <remarks>
    /// This method helps differentiate between a null reference and a destroyed Unity object. Unity's "== null" check
    /// can incorrectly return true for destroyed objects, leading to misleading behaviour. The OrNull method use
    /// Unity's "null check", and if the object has been marked for destruction, it ensures an actual null reference is returned,
    /// aiding in correctly chaining operations and preventing NullReferenceExceptions.
    /// </remarks>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object being checked.</param>
    /// <returns>The object itself if it exists and not destroyed, null otherwise.</returns>
    public static T OrNull<T> (this T obj) where T : Object => obj ? obj : null;
    
}