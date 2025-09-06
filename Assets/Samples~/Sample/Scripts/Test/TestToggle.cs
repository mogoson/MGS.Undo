/*************************************************************************
 *  Copyright (C) 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TestToggle.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2025/8/19
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace MGS.Undo.Sample
{
    [RequireComponent(typeof(Toggle))]
    public class TestToggle : MonoBehaviour
    {
        private Toggle toggle;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(Toggle_onValueChanged);
        }

        private void Toggle_onValueChanged(bool isOn)
        {
            void Undo()
            {
                toggle.SetIsOnWithoutNotify(!isOn);
            }
            void Redo()
            {
                toggle.SetIsOnWithoutNotify(isOn);
            }
            var handler = new DoHandler(Undo, Redo);
            Global.UndoManager.Register(handler);
        }
    }
}