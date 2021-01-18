using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using It_AcademyHomework.Repository.Common;

namespace Lesson7_Homework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogItemController : ControllerBase
    {
        private static IDictionary<string, IEnumerable<Good>> _catalogsWithItems =
            new Dictionary<string, IEnumerable<Good>>()
            {
                {"Phones", new List<Good>() { new Good() { Name = "1", Price = "230$"} }},
                {"Notebooks", new List<Good>() { new Good() { Name = "2", Price = "250$"}, new Good() { Name = "22", Price = "210$"} }},
                {"SmartWatches", new List<Good>() { new Good() { Name = "3", Price = "230$"} }},
                {"Other", new List<Good>() { new Good() { Name = "4", Price = "230$"} }},
            };

        [HttpPost]
        public IEnumerable<Good> Get([FromBody] Catalog catalog)
        {
            _catalogsWithItems.TryGetValue(catalog.Name, out var items);
            return items;
        }
    }
}
