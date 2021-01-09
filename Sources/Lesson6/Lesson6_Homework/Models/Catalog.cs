using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using It_AcademyHomework.Repository.Common;

namespace Lesson6_Homework.Models
{
    public class Catalog
    {
        public string Name { get; set; }

        public ICollection<Good> Goods { get; set; }
    }
}
