/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SampleUI.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/20/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Undo.Sample
{
    public class SampleUI : MonoBehaviour
    {
        public Transform itemRoot;
        public SampleItem itemPrefab;
        public Button btnAdd;
        public Button btnRemove;
        public Button btnClear;

        [Space]
        public ParticleSystem particle;
        public Button btnPlay;
        public Button btnStop;

        List<SampleItem> items = new();

        private void Awake()
        {
            btnAdd.onClick.AddListener(OnBtnAddClick);
            btnRemove.onClick.AddListener(OnBtnRemoveClick);
            btnClear.onClick.AddListener(OnBtnClearClick);

            btnPlay.onClick.AddListener(OnBtnPlayClick);
            btnStop.onClick.AddListener(OnBtnStopClick);
        }

        private void OnBtnAddClick()
        {
            var item = Instantiate(itemPrefab, itemRoot);
            item.Refresh(items.Count.ToString());
            item.OnClick += () => OnSampleItemClick(item);

            //UndoHandler for GameObject Instantiate.
            var handler = new UndoHandlerForGoInstantiate(
                () => items.Remove(item),
                () => items.Add(item),
                item.gameObject);
            Global.UndoManager.Todo(handler);
        }

        private void OnBtnRemoveClick()
        {
            if (items.Count > 0)
            {
                var last = items[items.Count - 1];

                //UndoHandler for GameObject Destroy.
                var handler = new UndoHandlerForGoDestroy(
                    () => items.Add(last),
                    () => items.Remove(last),
                    last.gameObject);
                Global.UndoManager.Todo(handler);
            }
        }

        private void OnBtnClearClick()
        {
            var temp = new List<SampleItem>(items);
            var gos = temp.ConvertAll(item => item.gameObject).ToArray();

            //UndoHandler for GameObject Destroy.
            var handler = new UndoHandlerForGoDestroy(
                () => items.AddRange(temp),
                () => items.Clear(),
                gos);
            Global.UndoManager.Todo(handler);
        }

        private void OnBtnPlayClick()
        {
            //UndoHandler with different action.
            var handler = new UndoHandler(particle.Stop, particle.Play);
            Global.UndoManager.Todo(handler);
        }

        private void OnBtnStopClick()
        {
            //UndoHandler with different action.
            var handler = new UndoHandler(particle.Play, particle.Stop);
            Global.UndoManager.Todo(handler);
        }

        private void OnSampleItemClick(SampleItem item)
        {
            var oldColor = item.image.color;
            var newColor = Random.ColorHSV();

            //UndoHandler with same action to change value.
            var handler = new UndoHandler<Color>(oldColor, newColor, color => item.image.color = color);
            Global.UndoManager.Todo(handler);
        }
    }
}