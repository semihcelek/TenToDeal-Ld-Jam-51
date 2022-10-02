using System;

namespace SemihCelek.TenToDeal.Model
{
    [Flags]
    public enum GameState
    {
        Idle = 0,
        TimerStarted = 1,
        TimerCycleCompleted = 2,
        SectionStarted = 4,
        SectionCompleted = 8,
        Failed = 16,
    }
}