using System;
using System.Runtime.CompilerServices;
using UI.Base;
using UI.Button;
using UnityEngine;

namespace UI
{
    public class SM_HUDPanel : SM_PanelBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string GetName() { return "HUD"; }

        [SerializeField] private SM_DefaultButton startStageButton;

        private void Start()
        {
            startStageButton.Initialize(ESM_DefaultButtonType.StartStage);
        }
    }
}