using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Cookie_Clicker.Runtime.Cookies.Infrastructure.Baker
{
    public class CursorsController : MonoBehaviour
    {
        [SerializeField] private RectTransform cursorPrefab;
        
        [Header("Settings")]
        [SerializeField] private float radius = 150;
        [SerializeField] private float offsetBetweenCircles = 20;
        [SerializeField] private int cursorsPerCircle = 50;
        [SerializeField] private float spinSpeed = 10f;
        
        private readonly Stack<RectTransform> _cursors = new  Stack<RectTransform>();
        
        private float OffsetBetweenCursors => (360f / cursorsPerCircle) * Mathf.Deg2Rad;
        private int _currentCursor;
        private int _currentCircle;

        private void Update()
        {
            Spin();
            DebugInputs();
        }

        private void Spin()
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }

        private void DebugInputs()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                AddCursor();
            if (Input.GetKeyDown(KeyCode.F2))
                RemoveCursor();
        }

        public void AddCursors(int amount)
        {
            Assert.IsTrue(amount > 0);

            for (int i = 0; i < amount; i++)
                AddCursor();
        }
        
        public void RemoveCursors(int amount)
        {
            Assert.IsTrue(amount > 0);

            for (int i = 0; i < amount; i++)
                RemoveCursor();
        }

        private void AddCursor()
        {
            var angle = OffsetBetweenCursors * _currentCursor;
            var actualRadius = radius + _currentCircle * offsetBetweenCircles;
            var pos = new Vector2(actualRadius * Mathf.Cos(angle), actualRadius * Mathf.Sin(angle));
            
            var cursor = Instantiate(cursorPrefab, transform);
            cursor.anchoredPosition = pos;
            cursor.transform.up = (transform.position - cursor.transform.position).normalized;
            _cursors.Push(cursor);
            
            _currentCursor++;
            if (_currentCursor >= cursorsPerCircle)
            {
                _currentCursor = 0;
                _currentCircle++;
            }
        }

        private void RemoveCursor()
        {
            if (_currentCursor <= 0 && _currentCircle <= 0) return;
            
            var cursor = _cursors.Pop();
            Destroy(cursor.gameObject);

            _currentCircle = _cursors.Count / cursorsPerCircle;
            _currentCursor = _cursors.Count % cursorsPerCircle;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}