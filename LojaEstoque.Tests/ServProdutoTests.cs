using AutoFixture.Xunit2;
using FakeItEasy;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Exceptions;
using LojaEstoque.Dominio.Services;
using LojaEstoque.Repositories.Interfaces;

namespace LojaEstoque.Tests
{
    public class ServProdutoTests
    {
        #region Cadastrar_DescricaoDuplicada_DeveLancarRegraDeNegocioException
        [Theory]
        [AutoData]
        public async Task Cadastrar_DescricaoDuplicada_DeveLancarRegraDeNegocioException(ProdutoDto produtoDto)
        {
            IRepProduto repProduto = A.Fake<IRepProduto>();

            A.CallTo(() => repProduto.ExisteDescricao(produtoDto.Descricao))
                .Returns(true);

            ServProduto servProduto = new ServProduto(repProduto);

            RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servProduto.Cadastrar(produtoDto));

            Assert.Equal("Já existe um produto com essa descrição", exception.Message);

            A.CallTo(() => repProduto.Cadastrar(A<Produto>._))
                .MustNotHaveHappened();
        }
        #endregion

        #region Cadastrar_DescricaoLivre_DeveCadastrarProduto
        [Theory]
        [AutoData]
        public async Task Cadastrar_DescricaoLivre_DeveCadastrarProduto(ProdutoDto produtoDto)
        {
            IRepProduto repProduto = A.Fake<IRepProduto>();

            A.CallTo(() => repProduto.ExisteDescricao(produtoDto.Descricao))
                .Returns(false);

            ServProduto servProduto = new ServProduto(repProduto);

            Produto? produto = await servProduto.Cadastrar(produtoDto);

            Assert.NotNull(produto);
            Assert.Equal(produtoDto.Descricao, produto.Descricao);
            Assert.Equal(produtoDto.PrecoUnitario, produto.PrecoUnitario);
            Assert.Equal(produtoDto.Quantidade, produto.Quantidade);

            A.CallTo(() => repProduto.Cadastrar(A<Produto>.That.Matches(x =>
                x.Descricao == produtoDto.Descricao &&
                x.PrecoUnitario == produtoDto.PrecoUnitario &&
                x.Quantidade == produtoDto.Quantidade)))
                .MustHaveHappenedOnceExactly();
        }
        #endregion

        #region Editar_IdInvalido_DeveLancarRegraDeNegocioException
        [Theory]
        [AutoData]
        public async Task Editar_IdInvalido_DeveLancarRegraDeNegocioException(Guid id, ProdutoDto produtoDto)
        {
            IRepProduto repProduto = A.Fake<IRepProduto>();

            A.CallTo(() => repProduto.BuscarPorId(id))
                .Returns(Task.FromResult<Produto?>(null));

            ServProduto servProduto = new ServProduto(repProduto);

            RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servProduto.Editar(id, produtoDto));

            Assert.Equal("Produto não encontrado", exception.Message);

            A.CallTo(() => repProduto.Editar(A<Produto>._))
                .MustNotHaveHappened();
        }
        #endregion

        #region Editar_DescricaoDuplicadaEmOutroProduto_DeveLancarRegraDeNegocioException
        [Theory]
        [AutoData]
        public async Task Editar_DescricaoDuplicadaEmOutroProduto_DeveLancarRegraDeNegocioException(Guid id, Produto produto, ProdutoDto produtoDto)
        {
            IRepProduto repProduto = A.Fake<IRepProduto>();

            produto.Id = id;

            A.CallTo(() => repProduto.BuscarPorId(id))
                .Returns(produto);

            A.CallTo(() => repProduto.ExisteDescricaoOutroProduto(produto.Id, produtoDto.Descricao))
                .Returns(true);

            ServProduto servProduto = new ServProduto(repProduto);

            RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servProduto.Editar(id, produtoDto));

            Assert.Equal("Já existe outro produto com essa descrição", exception.Message);

            A.CallTo(() => repProduto.Editar(A<Produto>._))
                .MustNotHaveHappened();
        }
        #endregion

        #region Editar_DadosValidos_DeveEditarProduto
        [Theory]
        [AutoData]
        public async Task Editar_DadosValidos_DeveEditarProduto(Guid id, Produto produto, ProdutoDto produtoDto)
        {
            IRepProduto repProduto = A.Fake<IRepProduto>();

            produto.Id = id;

            A.CallTo(() => repProduto.BuscarPorId(id))
                .Returns(produto);

            A.CallTo(() => repProduto.ExisteDescricaoOutroProduto(produto.Id, produtoDto.Descricao))
                .Returns(false);

            A.CallTo(() => repProduto.Editar(A<Produto>._))
                .ReturnsLazily((Produto produtoEditado) => produtoEditado);

            ServProduto servProduto = new ServProduto(repProduto);

            Produto produtoResultado = await servProduto.Editar(id, produtoDto);

            Assert.Equal(produtoDto.Descricao, produtoResultado.Descricao);
            Assert.Equal(produtoDto.PrecoUnitario, produtoResultado.PrecoUnitario);
            Assert.Equal(produtoDto.Quantidade, produtoResultado.Quantidade);

            A.CallTo(() => repProduto.Editar(A<Produto>.That.Matches(x =>
                x.Id == id &&
                x.Descricao == produtoDto.Descricao &&
                x.PrecoUnitario == produtoDto.PrecoUnitario &&
                x.Quantidade == produtoDto.Quantidade)))
                .MustHaveHappenedOnceExactly();
        }
        #endregion

        #region Remover_IdInvalido_DeveLancarRegraDeNegocioException
        [Theory]
        [AutoData]
        public async Task Remover_IdInvalido_DeveLancarRegraDeNegocioException(Guid id)
        {
            IRepProduto repProduto = A.Fake<IRepProduto>();

            A.CallTo(() => repProduto.BuscarPorId(id))
                .Returns(Task.FromResult<Produto?>(null));

            ServProduto servProduto = new ServProduto(repProduto);

            RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servProduto.Remover(id));

            Assert.Equal("Produto não encontrado", exception.Message);

            A.CallTo(() => repProduto.Remover(A<Guid>._))
                .MustNotHaveHappened();
        }
        #endregion
    }
}