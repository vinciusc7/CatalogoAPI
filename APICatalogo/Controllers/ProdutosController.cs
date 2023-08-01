using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using APICatalogo.Filter;

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
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                var produtos = await _context.Produtos.ToListAsync();
                if (produtos is null)
                {
                    return NotFound("Não existem produtos cadastrados!");
                }
                return produtos;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("teste/primeiro")]
        public async  Task<ActionResult<Produto>> Get2()
        {
            try
            {
                var produtos = await _context.Produtos.FirstOrDefaultAsync();
                
                if (produtos is null)
                {
                    return NotFound("Nenhum produto encontrado!");
                }
                return produtos;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("primeiro")]
        public async Task<ActionResult<Produto>> GetPrimeiroProduto()
        {
            try
            {
                var produtos = await _context.Produtos.ToListAsync();
                var produto = produtos.FirstOrDefault();
                if (produto is null)
                {
                    return NotFound("Não existem produtos cadastrados!");
                }
                return produto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("ByCategory/{id:int:min(1)}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetByCategory(int id, [BindRequired] string param2)
        {
            try
            {
                var parametro = param2;
                var produtos = await _context.Produtos?.Where(x => x.CategoriaID == id).ToListAsync();
                if (produtos is null)
                {
                    return NotFound("Nenhum produto atrelado a esta categoria");
                }

                return produtos;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            try
            {
                //throw new Exception("Exception ao retornar produto pelo id");

                var produto = await _context.Produtos?.FirstOrDefaultAsync(x => x.ProdutoId == id);
                if (produto is null)
                {
                    return BadRequest("Produto não encontrado!");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPost]
        public async Task<ActionResult> Post(Produto produto)
        {
            try
            {
                if (produto is null)
                {
                    return BadRequest("Nenhum produto inserido");
                }
                _context.Produtos?.Add(produto);
                await _context.SaveChangesAsync();
                return new CreatedAtRouteResult("ObterProduto",
                    new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest("Id do produto divergente do produto alterado!");
                }

                var validarProduto = _context.Produtos?.FirstOrDefault(p => p.ProdutoId == id);
                if (validarProduto is null)
                {
                    return NotFound("Nenhum produto encontrado com este ID!");
                }

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var produto = await _context.Produtos?.FirstOrDefaultAsync(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound("Nenhum produto encontrado com este ID!");
                }
                _context.Remove(produto);
                await _context.SaveChangesAsync();

                return Ok($"O produto '{produto.Nome?.ToString()}' deletado com sucesso!");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
