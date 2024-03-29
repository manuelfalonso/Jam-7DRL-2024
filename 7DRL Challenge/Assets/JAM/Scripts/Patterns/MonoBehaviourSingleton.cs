using UnityEngine;

namespace Utils.Singleton {
	public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour {
		protected static T m_Instance;

		protected static bool m_AttemptedSlowAccessOnceBool = false;

		public static T SlowAccessInstance {
			get {
				if (m_Instance == null)
				{
					m_Instance = FindObjectOfType<T>();
				}

				return Instance;
			}
		}

		public static T Instance {
			get {
				CheckForOneTimeSlowInstance();

				return m_Instance;
			}
		}

		protected static void CheckForOneTimeSlowInstance()
		{
			//One time, will try to access it slowly
			if (!m_AttemptedSlowAccessOnceBool)
			{
				m_AttemptedSlowAccessOnceBool = true;

				if (m_Instance == null)
				{
					m_Instance = SlowAccessInstance;
				}
			}
		}

		protected virtual void Awake()
		{
			if (this is T)
			{
				if (m_Instance == null || m_Instance == this)
				{
					m_Instance = this as T;
				}
				else
				{
					Debug.LogWarning("An instance of type [" + typeof(T) + "] already exists, and you are trying to create another one on Object [" + name + "].", m_Instance);
				}
			}
			else
			{
				Debug.LogError("MonoBehaviourSingleton has been initialised for type [" + this.GetType() + "], but the instance is for type [" + typeof(T) + "].");
			}
		}
	}
}