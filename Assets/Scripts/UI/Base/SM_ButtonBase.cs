using System;
using Systems;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    public abstract class SM_ButtonBase<TEnum> : MonoBehaviour where TEnum : Enum
    {
        [SerializeField] private Button _button;
        
        public TEnum ButtonType { get; protected set; }

        public void Initialize(TEnum buttonType)
        {
            ButtonType = buttonType;

            if (_button == null)
            {
                _button = GetComponent<Button>();
            }

            if (_button != null)
            {
                _button.onClick.AddListener(OnClick);
            }
            else
            {
                SM_Log.ERROR("[UIButtonBase] Button component missiong.");
            }
        }

        protected abstract void OnClick();

        protected void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnClick);
            }
        }
    }
}