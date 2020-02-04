using System;
using System.Collections;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GachiScripts
{
    public class Church : MonoBehaviour
    {
        private LineRenderer _ray;
        private GameObject _target;
        private SpriteRenderer _image;
        private ClickableUpgrade _upgrade;

        private int _upgradeLevel = 0;
        private float _cd = 1.0f;
        private const int MaxUp = 3;

        [SerializeField] private Spawner pedicSpawner;
        [SerializeField] private Sprite[] upgradeView;
        [SerializeField] private MilonovAss LandingZone;


        // Start is called before the first frame update
        private void Start()
        {
            _ray = GetComponentInChildren<LineRenderer>();
            _image = GetComponent<SpriteRenderer>();
            _upgrade = GetComponentInChildren<ClickableUpgrade>();
            _upgrade.SetTarget(this);
            _upgrade.SetCost(100);
            _upgrade.gameObject.SetActive(false);
            _ray.SetPosition(0, transform.position + Vector3.up * 0.2f);
        }

        // Update is called once per frame
        private void Update()
        {
            _target = null;

            if (LandingZone) 
                _target = FindTarget(LandingZone.transform);

            if (!_target)
                _target = FindTarget(pedicSpawner.transform);

            if (!_target || _upgradeLevel == 0)
                _ray.enabled = false;
            else
            {
                _ray.SetPosition(1, _target.transform.position);
                _ray.enabled = true;
            }

            _cd -= Time.deltaTime;
            if (_cd > 0) return;
            _cd = 1.0f;
            if (_upgradeLevel > 0 && _target)
                _target.GetComponent<Pidor>().Zhahnut(_upgradeLevel);
        }

        private GameObject FindTarget(Transform t)
        {
            GameObject result = null;
            for (var i = 0; i < t.childCount; ++i)
            {
                var pedic = t.GetChild(i);
                if ((transform.position - pedic.position).sqrMagnitude > 4) continue;
                result = pedic.gameObject;
                break;
            }

            return result;
        }

        public void Upgrade()
        {
            if (_upgradeLevel == MaxUp) return;
            _image.sprite = upgradeView[_upgradeLevel];
            _upgradeLevel++;
            _upgrade.SetCost(_upgradeLevel * 200);
        }

        private void OnMouseUpAsButton()
        {
            _upgrade.Shown = true;
            _upgrade.gameObject.SetActive(!_upgrade.gameObject.activeSelf);
        }
    }
}