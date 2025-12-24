/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  UndoHotKey.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  12/18/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Undo.Sample
{
    public class UndoHotKey : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Global.UndoManager.Undo();
                }
                else if (Input.GetKeyDown(KeyCode.Y))
                {
                    Global.UndoManager.Redo();
                }
            }
        }
    }
}