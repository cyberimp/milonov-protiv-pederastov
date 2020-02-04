using System.Collections;
using UnityEngine;

namespace GachiScripts
{
    public class Pidor : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private Animator _animator;
        private AudioSource _audio;
        private WayPointsController _waypoint;

        private int _hp = 5;

        private static readonly int Dead = Animator.StringToHash("Dead");

        // Start is called before the first frame update
        private void Start()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _audio = GetComponent<AudioSource>();
            _waypoint = GetComponent<WayPointsController>();
        }

        // Update is called once per frame
        private void Update()
        {
            _sprite.sortingOrder = -(int) (transform.position.y * 10f);
        }

        public void Zhahnut(int damage)
        {
            if ((_hp -= damage) <= 0)
                Die();
        }

        private void Die()
        {
            GoldCollector.Value += 5;
            _animator.SetBool(Dead, true);
            _audio.Play();
            transform.SetParent(null, true);
            Destroy(_waypoint);
            Destroy(gameObject, 1.045f);
        }
    }
}
