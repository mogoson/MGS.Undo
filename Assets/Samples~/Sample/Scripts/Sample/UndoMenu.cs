/*************************************************************************
 *  Copyright (C) 2025 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  UndoMenu.cs
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
    public class UndoMenu : MonoBehaviour
    {
        public Button undoBtn;
        public Button redoBtn;

        private void Awake()
        {
            Global.UndoManager.OnUndosChanged += UndoManager_OnUndosChanged;
            Global.UndoManager.OnRedosChanged += UndoManager_OnRedosChanged;

            undoBtn.onClick.AddListener(() => Global.UndoManager.Undo());
            redoBtn.onClick.AddListener(() => Global.UndoManager.Redo());
        }

        private void OnDestroy()
        {
            Global.UndoManager.OnUndosChanged -= UndoManager_OnUndosChanged;
            Global.UndoManager.OnRedosChanged -= UndoManager_OnRedosChanged;
        }

        private void UndoManager_OnUndosChanged(int count)
        {
            undoBtn.interactable = count > 0;
        }

        private void UndoManager_OnRedosChanged(int count)
        {
            redoBtn.interactable = count > 0;
        }
    }
}