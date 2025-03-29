using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PageManagements.Pages
{
    public class MultipleButtonsPage : MonoBehaviour, IPage
    {
        private bool _shouldClose;

        /// <summary>
        /// The event that is called when a button is pressed.
        /// It passes the key indicating which button was pressed as an argument.
        /// </summary>
        public event Action<string> OnClickButton;

        public bool IsKeepPreviousPage => true;

        public void Dispose()
        {
            Destroy(gameObject);
        }

        public UniTask Show(CancellationToken cancellationToken)
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public bool ShouldClose() => _shouldClose;

        public UniTask Hide(CancellationToken cancellationToken)
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public void OnClick(string key)
        {
            OnClickButton?.Invoke(key);
        }

        public void OnClose()
        {
            _shouldClose = true;
        }
    }
}
