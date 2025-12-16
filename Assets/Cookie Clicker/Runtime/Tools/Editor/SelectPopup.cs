using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Cookie_Clicker.Runtime.Tools.Editor
{
    public class SelectPopup<T>
    {
        private List<T> _options;
        private HashSet<T> _selected;
        private readonly Action<List<T>> _onChange;
        
        public SelectPopup(List<T> options, List<T> initialValues, Action<List<T>> onChange)
        {
            _options = options;
            _selected = new HashSet<T>(initialValues);
            _onChange = onChange;
        }

        public void Draw(string label)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(label);

            var selectorLabel = GetSelectorLabel();
            if (GUILayout.Button(selectorLabel, EditorStyles.popup))
            {
                var menu = new GenericMenu();
                
                menu.AddItem(new GUIContent("All"), _selected.Count == _options.Count, () =>
                {
                    _selected.Clear();
                    foreach (var opt in _options)
                        _selected.Add(opt);
                    _onChange.Invoke(new List<T>(_selected));
                });
                
                menu.AddSeparator("");

                foreach (var opt in _options)
                {
                    var isSelected = _selected.Contains(opt) && _selected.Count == 1;
                    menu.AddItem(new GUIContent(opt.ToString()), isSelected, () =>
                    {
                        _selected.Clear();
                        _selected.Add(opt);
                        _onChange.Invoke(new List<T>(_selected));
                    });
                }
                
                menu.ShowAsContext();
            }
            EditorGUILayout.EndHorizontal();
        }

        public void SetOptions(List<T> options)
        {
            _options = options;
            _selected = new HashSet<T>(options);
            _onChange.Invoke(new List<T>(_selected));
        }

        private string GetSelectorLabel()
        {
            return _selected.Count == _options.Count ? "All" : _selected.First().ToString();
        }
    }
}