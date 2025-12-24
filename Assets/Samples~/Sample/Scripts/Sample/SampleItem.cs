/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SampleItem.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/20/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Undo.Sample
{
    public class SampleItem : MonoBehaviour
    {
        public Button button;
        public Image image;
        public Text text;

        public event Action OnClick;

        private void Awake()
        {
            button.onClick.AddListener(() => OnClick?.Invoke());
        }

        public void Refresh(string text)
        {
            this.text.text = text;
        }
    }
}