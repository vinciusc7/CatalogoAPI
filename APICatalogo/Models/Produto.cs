using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }
   
    [Required(ErrorMessage ="O nome é obrigatório")]
    [StringLength(85, ErrorMessage ="O nome deve ter entre 5 ou 20 caracteres",
        MinimumLength = 5)]
    public string? Nome { get; set; }
    
    [Required]
    /*[StringLength(10, ErrorMessage = "A descrição deve ter no máximo {1} caractere",
        MinimumLength = 1)]*/
    public string? Descricao { get; set; }
    
    [Required]
    /*[Range(1, 10000, ErrorMessage ="O preço deve estar entre {1} e {2}")]*/
    [Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }
    
    [Required]
    [StringLength(350, MinimumLength = 10)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaID { get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Nome != null || this.Nome != string.Empty)
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("A primeira letra do produto deve ser maíscula ",
                    new[]
                    {  nameof(this.Nome) });
            }
        }

        if (this.Preco <= 0)
        {
            yield return new ValidationResult("O valor do produto deve ser maior que 0",
                new[]
                { nameof(this.Preco) });
        }
        if (this.Estoque <= 0)
        {
            yield return new ValidationResult("Deve existir 1 produto ou mais no estoque",
                new[]
                { nameof(this.Estoque) });
        }

    }
}
