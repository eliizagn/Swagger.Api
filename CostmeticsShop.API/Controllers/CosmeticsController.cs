using App.Metrics;
using CosmeticsShop.API;
using CostmeticsShop.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CosmeticsShop.Controllers
{
    
    [ApiController]
    [Route("/api/v1/cosmetics")]
    public class CosmeticsController : ControllerBase
    {
        private readonly IMetrics _metrics;
        private readonly ILogger _logger;

        public static List<Cosmetic> Cosmetics = new List<Cosmetic>
        {
            new Cosmetic
            {
                Id = 1,
                Name = "Blush"
            },
            new Cosmetic
            {
                Id = 2,
                Name = "Highlighter"
            },
                new Cosmetic
            {
                Id = 3,
                Name = "Powder"
            },
                new Cosmetic
            {
                Id = 4,
                Name = "Lipstick"
            },
                new Cosmetic
            {
                Id = 5,
                Name = "Eyeliner"
            }
        };

        public CosmeticsController(IMetrics metrics, ILogger<CosmeticsController> logger)
        {
            _metrics = metrics;
            _logger = logger;
        }

        /// <summary>
        ///  метод получения продукта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        [HttpGet("{id:int}")]
        public Cosmetic Get(int id)
        {
            _metrics.Measure.Counter.Increment(MetricsRegister.FindCosmeticProduct);
            var cosmetic = Cosmetics.Find(x => x.Id == id);
            return cosmetic;
        }


        /// <summary>
        ///  метод получения информации о всех продуктах
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        [HttpGet]
        public List<Cosmetic> GetAll() => Cosmetics;


        /// <summary>
        /// метод создания косметического продукта
        /// </summary>
        /// <param name="body"></param>

        [HttpPost]
        public ActionResult Create([FromBody] Cosmetic model)
        {
            _metrics.Measure.Counter.Increment(MetricsRegister.CreateCosmeticsProduct);
            var cosmetic = new Cosmetic
            {
                Id = model.Id,
                Name = model.Name,
            };
            Cosmetics.Add(cosmetic);
            return Ok();
        }
           
        
        /// <summary>
        /// метод удаления косметического продукта по идентификатору
        /// </summary>
        /// <param name="cosmeticId">Идентификатор продукта</param>
       
        [HttpDelete("Id:int")]
        public void Delete(int ID) => Cosmetics.RemoveAt(ID); 

    }
}


