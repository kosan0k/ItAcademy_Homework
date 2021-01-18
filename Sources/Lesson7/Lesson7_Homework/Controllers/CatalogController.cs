using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using It_AcademyHomework.Repository.Common;

namespace Lesson7_Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        public static readonly Catalog[] _catalogs = new[]
        {
            new Catalog() { Name = "Phones"},
            new Catalog() { Name = "Notebooks"},
            new Catalog() { Name = "SmartWatches"},
            new Catalog() { Name = "Other"}
        };

        [HttpGet]
        public IEnumerable<Catalog> Get()
        {
            return _catalogs.ToArray();
        }
    }
}
