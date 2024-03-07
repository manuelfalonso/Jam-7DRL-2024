using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JAM
{
    public class TurnUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        void Start()
        {
            _text.text = "Turn: " + TurnSystem.Instance.GetTurnNumber();
            TurnSystem.Instance.onTurnStart += UpdateTurnNumber;
        }

        private void UpdateTurnNumber() 
        {
            _text.text = "Turn: " + TurnSystem.Instance.GetTurnNumber();
        }
    }
}
