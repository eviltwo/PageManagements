using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PageManagements
{
    public abstract class PageBase : MonoBehaviour, IDisposable
    {
        protected PageArgument PageArg { get; private set; }

        public virtual UniTask Initialize(PageArgument arg, CancellationToken cancellationToken)
        {
            PageArg = arg;
            return UniTask.CompletedTask;
        }

        public abstract void Show();

        public abstract void Hide();

        public abstract void Dispose();
    }
}
