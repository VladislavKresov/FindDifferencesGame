public class CountDownTickSignal {
    public readonly float Tick;
    public readonly float TimeLimit;
    public readonly float MillisecondsLeft;

    public CountDownTickSignal(float tick, float timeLimit, float millisecondsLeft) {
        Tick = tick;
        TimeLimit = timeLimit;
        MillisecondsLeft = millisecondsLeft;
    }
}
