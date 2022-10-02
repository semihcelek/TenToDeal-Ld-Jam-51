using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace SemihCelek.TenToDeal.Utilites
{
    public class UniTaskHelper
    {
        public static UniTask Delay(float secondsToDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayTimeSpan = TimeSpan.FromSeconds(secondsToDelay);
            return UniTask.Delay(delayTimeSpan, ignoreTimeScale, delayTiming, cancellationToken);
        }
    }
}