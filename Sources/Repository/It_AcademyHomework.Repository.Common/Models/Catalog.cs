using System.Collections.Generic;
using It_AcademyHomework.Repository.Common.Models;

namespace It_AcademyHomework.Repository.Common
{
    public class Catalog : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Good> Goods { get; set; }
    }
}
