using System;

namespace SemihCelek.TenToDeal.Model
{
    [Flags]
    public enum GameState
    {
        Idle = 0,
        TimerStarted = 1,
        TimerCycleCompleted = 2,
        SectionCompleted = 4,
        Failed = 8,
    }
}