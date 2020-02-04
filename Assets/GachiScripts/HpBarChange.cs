using UnityEngine;
using UnityEngine.UI;

namespace GachiScripts
{
    public class HpBarChange : MonoBehaviour
    {
        private Image _sprite;

        public int Hp
        {
            get => (int) (_sprite.fillAmount * 100f);
            set => _sprite.fillAmount = value / 100f;
        }

        // Start is called before the first frame update
        void Start()
        {
            _sprite = GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}