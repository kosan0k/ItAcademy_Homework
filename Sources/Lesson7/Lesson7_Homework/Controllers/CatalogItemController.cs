using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using It_AcademyHomework.Repository.Common;

namespace Lesson7_Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogItemController : ControllerBase
    {
        private readonly IGenericRepository<Good> _goodsRepository;
        private readonly IGenericRepository<Catalog> _catalogRepository;

        public CatalogItemController(IGenericRepository<Good> goodsRepository, IGenericRepository<Catalog> catalogRepository)
        {
            _goodsRepository = goodsRepository ?? throw new ArgumentNullException(nameof(goodsRepository));
            _catalogRepository = catalogRepository ?? throw new ArgumentNullException(nameof(catalogRepository));
        }

        [HttpPost]
        public async Task<IEnumerable<Good>> Get([FromBody] Catalog catalog)
        {
            var catalogFromDb = (await _catalogRepository.GetAsync(c => c.Name.Equals(catalog.Name))).FirstOrDefault();
            return catalogFromDb != null
                ? await _goodsRepository.GetAsync(g => g.CatalogId == catalogFromDb.Id)
                : new List<Good>();
        }
    }
}
