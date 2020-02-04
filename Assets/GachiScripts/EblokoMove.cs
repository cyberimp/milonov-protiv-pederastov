using UnityEngine;

namespace GachiScripts
{
    public class EblokoMove : MonoBehaviour
    {
        [SerializeField] private int layer;

        private SpriteRenderer _sprite;

        private float _speed;
    
        // Start is called before the first frame update
        private void Start()
        {
            transform.localScale = Vector3.one / layer;
            _sprite = GetComponent<SpriteRenderer>();
            _sprite.color = new Color(1, 1, 1, 1 / (float) layer);
            _sprite.sortingOrder = layer;
            _speed = 1 / (float) layer;
        }

        // Update is called once per frame
        private void Update()
        {
            transform.position += Vector3.left * (_speed * Time.deltaTime);
            if (transform.position.x > -7) return;
            var transform1 = transform;
            var pos = transform1.position;
            pos.x = -pos.x;
            transform1.position = pos;
        }
    }
}
