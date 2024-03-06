using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JAM
{
    public class TurnButton : MonoBehaviour
    {
        public void PassTurn() 
        {
            TurnSystem.Instance.onTurnEnd?.Invoke();
        }
    }
}
