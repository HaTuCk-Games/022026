namespace DefaultNamespace.Components.Interfaces
{
    public interface ILevelUp
    {
    void LevelUp(CharacterData data, int level);
        public int MinLevel { get; }
    }
}
