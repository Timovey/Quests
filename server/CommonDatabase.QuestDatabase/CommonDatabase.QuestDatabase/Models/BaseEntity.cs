namespace CommonDatabase.QuestDatabase.Models
{
    internal class BaseEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
