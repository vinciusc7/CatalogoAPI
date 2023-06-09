﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
   
    [Required]
    [StringLength(85)]
    public string? Nome { get; set; }
    
    [Required]
    [StringLength(350)]
    public string? Descricao { get; set; }
    
    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }
    
    [Required]
    [StringLength(350)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaID { get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }

}
