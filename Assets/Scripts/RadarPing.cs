using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class RadarPing : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private float _dissppearTimer;
        private float _dissppearTimerMax;
        private Color _color;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _dissppearTimer = 0f;
            _dissppearTimerMax = 1f;
            _color = new Color(1, 1, 1, 1f);
        }

        void Update()
        {
            _dissppearTimer += Time.deltaTime;
            _color.a = Mathf.Lerp(_dissppearTimerMax, 0f, _dissppearTimer / _dissppearTimerMax);
            _spriteRenderer.color = _color;
            if (_dissppearTimer >= _dissppearTimerMax) Destroy(gameObject);
        }
    
        public void SetColor(Color _color)
        { this._color = _color; }

        public void SetDisappearTimer(float _dissppearTimerMax)
        {
            this._dissppearTimerMax = _dissppearTimerMax;
            _dissppearTimer = 0f;
        }       
    }
}
