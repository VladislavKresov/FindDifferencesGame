public class LevelStartSignal {
    public readonly int Level;
    public readonly float TimeLimit;
    public readonly int HiddenObjectsCount;

    public LevelStartSignal(int level, float timeLimit, int hiddenObjectsCount) {
        Level = level;
        TimeLimit = timeLimit;
        HiddenObjectsCount = hiddenObjectsCount;
    }
}
