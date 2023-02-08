namespace CommonDatabase.QuestDatabase.Models.Stages
{
    internal abstract class StageEntity : BaseEntity
    {
        internal string Title { get; set; }

        internal byte StageType { get; set; }

        internal QuestEntity Quest { get; set; }
    }
}
