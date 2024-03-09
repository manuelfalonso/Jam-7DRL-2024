using System;
using UnityEngine;

namespace JAM
{
    /// <summary>
    /// Use a collider attached to the object to trigger events in one layer.
    /// Events:
    /// - onEnterAction
    /// - onExitAction
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ProximityTrigger : MonoBehaviour
    {
        //[SerializeField]
        public LayerMask layer;
        [SerializeField]
        private Collider myCollider;

        public Transform otherTransform;
        public bool isTargetInside = false;

        [SerializeField]
        private UnityEngine.Events.UnityEvent<Collider> onEnterAction;
        [SerializeField]
        private UnityEngine.Events.UnityEvent<Collider> onExitAction;

        // private int Layer = -1;

        public Collider MyCollider => myCollider;


        private void Awake()
        {
            if (myCollider == null)
                myCollider = GetComponent<Collider>();
            // Layer = LayerMask.NameToLayer(layer);
            RefreshCollider();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (!(layer == (layer | (1 << other.gameObject.layer))))
            //if (!other.gameObject.layer.Equals(Layer))
            {
                //Debug.LogWarning(
                //    $"For best performance make collision only for desired layer" +
                //    $"Project Settings -> Physics/2D -> Layer collision matrix" +
                //    $" other: " + other.gameObject, gameObject);
                return;
            }

            otherTransform = other.transform;
            onEnterAction?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!(layer == (layer | (1 << other.gameObject.layer))))
                return;

            isTargetInside = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!(layer == (layer | (1 << other.gameObject.layer))))
                return;

            onExitAction?.Invoke(other);
            otherTransform = null;
            isTargetInside = false;
        }


        public void SetProximityTriggers(Action<Collider> onEnter, Action<Collider> onExit)
        {
            if (onEnter != null)
            {
                onEnterAction.AddListener((Collider other) => { onEnter(other); });
            }

            if (onExit != null)
            {
                onExitAction.AddListener((Collider other) => { onExit(other); });
            }
            myCollider.enabled = true;
        }

        public void RemoveProximityTriggers(Action<Collider> onEnter, Action<Collider> onExit)
        {
            if (onEnter != null)
            {
                onEnterAction.RemoveListener((Collider other) => { onEnter(other); });
            }

            if (onExit != null)
            {
                onExitAction.RemoveListener((Collider other) => { onExit(other); });
            }
            RefreshCollider();
        }

        public void ClearTriggers()
        {
            onEnterAction.RemoveAllListeners();
            onExitAction.RemoveAllListeners();
            myCollider.enabled = false;
        }


        void RefreshCollider()
        {
            myCollider.enabled = !(onEnterAction.GetPersistentEventCount() == 0 && onExitAction.GetPersistentEventCount() == 0);
        }
    }
}
//EOF.