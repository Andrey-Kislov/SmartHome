using System;
using System.Threading;

namespace Andead.SmartHome.Mqtt
{
    public sealed class SystemCancellationToken : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public SystemCancellationToken()
        {
            Token = _cancellationTokenSource.Token;
        }

        public CancellationToken Token { get; }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel(false);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}
