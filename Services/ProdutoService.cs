using ProdutosCodeFirst.Interfaces;
using ProdutosCodeFirst.Models;

namespace ProdutosCodeFirst.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> ListarTodosAsync()
        {
            return await _repository.ObterTodosAsync();
        }

        public async Task<Produto?> BuscarPorIdAsync(int id)
        {
            return await _repository.ObterPorIdAsync(id);
        }

        public async Task CadastrarAsync(Produto produto)
        {
            if (produto.Preco <= 0)
            {
                throw new Exception("O preço do produto deve ser maior que zero.");
            }

            await _repository.CriarAsync(produto);
        }

        public async Task AtualizarAsync(int id, Produto produto)
        {
            var produtoExistente = await _repository.ObterPorIdAsync(id);
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Marca = produto.Marca;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.QuantidadeEstoque = produto.QuantidadeEstoque;
            produtoExistente.Ativo = produto.Ativo;

            await _repository.AtualizarAsync(produtoExistente);
        }

        public async Task DeletarAsync(int id)
        {
            var produtoExistente = await _repository.ObterPorIdAsync(id);
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            await _repository.DeletarAsync(id);
        }
    }
}