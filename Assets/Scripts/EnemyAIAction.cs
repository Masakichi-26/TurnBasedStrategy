public class EnemyAIAction
{
    public GridPosition gridPosition { get; }
    public int actionValue { get; }

    public EnemyAIAction(GridPosition pos, int actionValue)
    {
        gridPosition = pos;
        this.actionValue = actionValue;
    }
}
