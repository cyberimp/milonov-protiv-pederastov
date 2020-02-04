using TMPro;
using UnityEngine;

namespace GachiScripts
{
    public class GoldCollector : MonoBehaviour
    {
        public static int Value = 300;
        private int _curGold;
        private TextMeshProUGUI _text;

        // Start is called before the first frame update
        void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_curGold == Value) return;
            _text.text = Value.ToString();
            _curGold = Value;
        }
    }
}
