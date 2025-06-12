using System.Runtime.CompilerServices;
using Systems;
using UI.Base;
using UI.Button;
using UnityEngine;

namespace UI
{
    public class SM_LobbyPanel : SM_PanelBase
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public static string GetName() { return "Lobby"; }

        [SerializeField] private SM_DefaultButton startGameButton;

        private void Start()
        {
            startGameButton.Initialize(ESM_DefaultButtonType.Start);
        }
    }
}