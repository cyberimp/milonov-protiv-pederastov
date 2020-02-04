using UnityEngine;
using UnityEngine.EventSystems;

namespace GachiScripts
{
    public class MilonovAss : MonoBehaviour
    {
        [SerializeField] private HpBarChange life;
        private float _lastCheckTime;
        private Animator _animator;
        private static readonly int Ebut = Animator.StringToHash("ebut");

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void AddPedic(GameObject eventData)
        {
            eventData.transform.SetParent(transform, true);
            //_animator.SetBool(Ebut, true);
        }

        // Update is called once per frame
        private void Update()
        {
            _lastCheckTime += Time.deltaTime;
            if (_lastCheckTime < 1.0f) return;
            _lastCheckTime = 0.0f;
            var pedics = transform.childCount;
            life.Hp -= pedics;
            _animator.SetBool(Ebut, pedics > 0);
        }
    }
}
