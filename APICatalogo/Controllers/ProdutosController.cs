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
            var produtos = _context.Produtos.Take(2).ToList();
            if (produtos is null)
            {
                return NotFound("Não existem produtos cadastrados!");
            }
            return produtos;
        }
        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
            if (produto is null)
            {
                return BadRequest("Produto não encontrado!");
            }
            return produto;
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Nenhum produto inserido");
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Id do produto divergente do produto alterado!");
            }

            var validarProduto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (validarProduto is null)
            {
                return NotFound("Nenhum produto encontrado com este ID!");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Nenhum produto encontrado com este ID!");
            }
            _context.Entry(produto).State = EntityState.Deleted;
            _context.SaveChanges();

            return Ok($"O produto {produto.Nome.ToString()} deletado com sucesso!");
        }
    }
}
