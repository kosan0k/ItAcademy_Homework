using It_AcademyHomework.Repository.Common.Models;

namespace It_AcademyHomework.Repository.Common
{
    public class Good : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }

        public int CatalogId { get; set; }
    }
}
