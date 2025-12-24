/*************************************************************************
 *  Copyright © 2025 Mogoson. All rights reserved.
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
        public Button btnUndo;
        public Button btnRedo;

        private void Awake()
        {
            Global.UndoManager.OnUndosChanged += UndoManager_OnUndosChanged;
            Global.UndoManager.OnRedosChanged += UndoManager_OnRedosChanged;

            btnUndo.onClick.AddListener(() => Global.UndoManager.Undo());
            btnRedo.onClick.AddListener(() => Global.UndoManager.Redo());
        }

        private void OnDestroy()
        {
            Global.UndoManager.OnUndosChanged -= UndoManager_OnUndosChanged;
            Global.UndoManager.OnRedosChanged -= UndoManager_OnRedosChanged;
        }

        private void UndoManager_OnUndosChanged(int count)
        {
            btnUndo.interactable = count > 0;
        }

        private void UndoManager_OnRedosChanged(int count)
        {
            btnRedo.interactable = count > 0;
        }
    }
}