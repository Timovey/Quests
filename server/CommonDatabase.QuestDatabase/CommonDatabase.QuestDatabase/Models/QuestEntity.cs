using CommonDatabase.QuestDatabase.Models.Stages;

namespace CommonDatabase.QuestDatabase.Models
{
    public class QuestEntity : BaseEntity
    {
        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public int StageCount => Stages.Count;

        public IList<StageEntity> Stages { get; set; }
    }
}
