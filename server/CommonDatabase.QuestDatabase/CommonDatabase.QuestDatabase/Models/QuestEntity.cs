using CommonDatabase.QuestDatabase.Models.Stages;

namespace CommonDatabase.QuestDatabase.Models
{
    internal class QuestEntity : BaseEntity
    {
        internal string Title { get; set; }

        internal string Description { get; set; }

        internal string Img { get; set; }

        internal IList<StageEntity> Stages { get; set; }
    }
}
