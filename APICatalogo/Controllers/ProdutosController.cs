﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Context;
using APICatalogo.Models;

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
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
            {
                return NotFound("Não existem produtos cadastrados!");
            }
            return produtos;
        }
        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int id)
        {

            var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
            if (produto is null)
            {
                return BadRequest("Produto não encontrado!");
            }
            return produto;
        }
    }
}
