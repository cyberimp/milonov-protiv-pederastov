using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace GachiScripts
{
    public class ClickableUpgrade : MonoBehaviour, IDeselectHandler
    {
        [NonSerialized] public bool Shown;
        [SerializeField] private Sprite enabledSprite;
        [SerializeField] private Sprite disabledSprite;
        private SpriteRenderer _renderer;
        private Church _tower;
        private int _cost;
        
        private void Awake()
        {
           // EventSystem.current.SetSelectedGameObject(gameObject);
        }

        private void OnEnable()
        {
            if (Shown)
                EventSystem.current.SetSelectedGameObject(gameObject);
        }

        // Start is called before the first frame update
        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            _renderer.sprite = GoldCollector.Value < _cost ? disabledSprite : enabledSprite;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (!Shown) return;
            gameObject.SetActive(false);
            Shown = false;
            eventData.Use();
        }
        
        private void OnMouseUpAsButton()
        {
            if (GoldCollector.Value < _cost) return;
            GoldCollector.Value -= _cost;
            _tower.Upgrade();
            gameObject.SetActive(false);
            Shown = false;
        }

        public void SetTarget(Church church)
        {
            _tower = church;
        }

        public void SetCost(int i)
        {
            _cost = i;
        }
    }
}
