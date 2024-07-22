using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace PageManagements
{
    public interface IPage : IDisposable
    {
        UniTask Show(CancellationToken cancellationToken);
        bool ShouldClose();
        UniTask Hide(CancellationToken cancellationToken);
    }
}
