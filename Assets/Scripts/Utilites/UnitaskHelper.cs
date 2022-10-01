using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Utilites
{
    public class UnitaskHelper
    {
        public static UniTask Delay(float secondsToDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken))
        {
            var delayTimeSpan = TimeSpan.FromSeconds(secondsToDelay);
            return UniTask.Delay(delayTimeSpan, ignoreTimeScale, delayTiming, cancellationToken);
        }
    }
}