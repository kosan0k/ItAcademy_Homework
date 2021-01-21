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
        private readonly IGenericRepository<Catalog> _catalogRepository;

        public CatalogController(IGenericRepository<Catalog> catalogRepository)
        {
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        [HttpGet]
        public async Task<IEnumerable<Catalog>> Get()
        {
            var catalogs = await _catalogRepository.GetAsync();
            return catalogs;
        }
    }
}
