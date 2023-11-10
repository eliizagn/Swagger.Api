using CosmeticsShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace CosmeticsShop.Controllers
{
    [ApiController]
    [Route("/api/v1/cosmetics")]
    public class CosmeticsController : ControllerBase
    {
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

        /// <summary>
        ///  метод получения продукта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор продукта</param>
        [HttpGet("{id:int}")]
        public Cosmetic Get(int id)
        {
            var temp = Cosmetics.Find(x => x.Id == id);
            return temp;
        }

        [HttpGet]
        public List<Cosmetic> GetAll() => Cosmetics;

        /// <summary>
        /// метод создания косметического продукта
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">Успешный ответ с созданным косметическим продуктом</response>
        /// <response code="0">Нестандартный запрос</response>
        [HttpPost]
        public void Create([FromBody] Cosmetic cosmetic) => Cosmetics.Add(cosmetic);

        /// <summary>
        /// метод удаления косметического продукта по идентификатору
        /// </summary>
        /// <param name="cosmeticId">Идентификатор продукта</param>
        /// <response code="200">Успешное удаление продукта</response>
        /// <response code="0">Нестандартный запрос</response>

        [HttpDelete("Id:int")]
        public void Delete(int ID) => Cosmetics.RemoveAt(ID); 

    }
}


