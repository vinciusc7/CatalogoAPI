using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try {
                var produtos = _context.Produtos?.Take(2).ToList();
                if (produtos is null) {
                    return NotFound("Não existem produtos cadastrados!");
                }
                return produtos;
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet("primeiro")]
        public ActionResult<Produto> GetPrimeiroProduto() {
            try {
                var produto = _context.Produtos?.FirstOrDefault();
                if (produto is null) {
                    return NotFound("Não existem produtos cadastrados!");
                }
                return produto;
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("ByCategory/{id:int:min(1)}")]
        public ActionResult<IEnumerable<Produto>> GetByCategory(int id) {
            try {
                var produtos = _context.Produtos?.Where(x => x.CategoriaID == id).ToList();
                if (produtos is null) {
                    return NotFound("Nenhum produto atrelado a esta categoria");
                }

                return produtos;
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet("{id:int:min(1)}", Name ="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            try {
                var produto = _context.Produtos?.FirstOrDefault(x => x.ProdutoId == id);
                if (produto is null) {
                    return BadRequest("Produto não encontrado!");
                }
                return produto;
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            try {
                if (produto is null) {
                    return BadRequest("Nenhum produto inserido");
                }
                _context.Produtos?.Add(produto);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterProduto",
                    new { id = produto.ProdutoId }, produto);
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Produto produto)
        {
            try {
                if (id != produto.ProdutoId) {
                    return BadRequest("Id do produto divergente do produto alterado!");
                }

                var validarProduto = _context.Produtos?.FirstOrDefault(p => p.ProdutoId == id);
                if (validarProduto is null) {
                    return NotFound("Nenhum produto encontrado com este ID!");
                }

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(produto);
            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            try {
                var produto = _context.Produtos?.FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null) {
                    return NotFound("Nenhum produto encontrado com este ID!");
                }
                _context.Entry(produto).State = EntityState.Deleted;
                _context.SaveChanges();

                return Ok($"O produto {produto.Nome?.ToString()} deletado com sucesso!");
            }
            catch (Exception) {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
