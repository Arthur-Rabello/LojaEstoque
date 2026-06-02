using AutoFixture.Xunit2;
using FakeItEasy;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Exceptions;
using LojaEstoque.Dominio.Services;
using LojaEstoque.Repositories.Interfaces;

namespace LojaEstoque.Tests
{
	public class ServCarrinhoTests
	{
		#region Cadastrar_ProdutoInexistente_DeveLancarRegraDeNegocioException
		[Theory]
		[AutoData]
		public async Task Cadastrar_ProdutoInexistente_DeveLancarRegraDeNegocioException(CarrinhoDto carrinhoDto)
		{
			IRepProduto repProduto = A.Fake<IRepProduto>();
			IRepCarrinho repCarrinho = A.Fake<IRepCarrinho>();

			A.CallTo(() => repProduto.BuscarPorId(carrinhoDto.ProdutoId))
				.Returns(Task.FromResult<Produto>(null!));

			ServCarrinho servCarrinho = new ServCarrinho(repProduto, repCarrinho);

			RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servCarrinho.Cadastrar(carrinhoDto));

			Assert.Equal("Produto não encontrado", exception.Message);

			A.CallTo(() => repCarrinho.Cadastrar(A<Carrinho>._))
				.MustNotHaveHappened();

			A.CallTo(() => repProduto.Editar(A<Produto>._))
				.MustNotHaveHappened();
		}
		#endregion

		#region Cadastrar_EstoqueInsuficiente_DeveLancarRegraDeNegocioException
		[Theory]
		[AutoData]
		public async Task Cadastrar_EstoqueInsuficiente_DeveLancarRegraDeNegocioException(CarrinhoDto carrinhoDto, Produto produto)
		{
			IRepProduto repProduto = A.Fake<IRepProduto>();
			IRepCarrinho repCarrinho = A.Fake<IRepCarrinho>();

			carrinhoDto.ProdutoId = produto.Id;
			carrinhoDto.Quantidade = 10;
			produto.Quantidade = 5;

			A.CallTo(() => repProduto.BuscarPorId(carrinhoDto.ProdutoId))
				.Returns(produto);

			ServCarrinho servCarrinho = new ServCarrinho(repProduto, repCarrinho);

			RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servCarrinho.Cadastrar(carrinhoDto));

			Assert.Equal("Quantidade em estoque insuficiente.", exception.Message);

			A.CallTo(() => repCarrinho.Cadastrar(A<Carrinho>._))
				.MustNotHaveHappened();

			A.CallTo(() => repProduto.Editar(A<Produto>._))
				.MustNotHaveHappened();
		}
		#endregion

		#region Cadastrar_DadosValidos_DeveCadastrarCarrinho
		[Theory]
		[AutoData]
		public async Task Cadastrar_DadosValidos_DeveCadastrarCarrinho(CarrinhoDto carrinhoDto, Produto produto)
		{
			IRepProduto repProduto = A.Fake<IRepProduto>();
			IRepCarrinho repCarrinho = A.Fake<IRepCarrinho>();

			carrinhoDto.ProdutoId = produto.Id;
			carrinhoDto.Quantidade = 2;

			produto.Quantidade = 10;
			produto.PrecoUnitario = 15.50m;

			A.CallTo(() => repProduto.BuscarPorId(carrinhoDto.ProdutoId))
				.Returns(produto);

			ServCarrinho servCarrinho = new ServCarrinho(repProduto, repCarrinho);

			Carrinho? carrinho = await servCarrinho.Cadastrar(carrinhoDto);

			Assert.NotNull(carrinho);
			Assert.Equal(produto.Id, carrinho.ProdutoId);
			Assert.Equal(carrinhoDto.Quantidade, carrinho.Quantidade);
			Assert.Equal(produto.PrecoUnitario, carrinho.PrecoUnitario);
			Assert.Equal(31.00m, carrinho.PrecoTotal);

			A.CallTo(() => repCarrinho.Cadastrar(A<Carrinho>.That.Matches(x =>
				x.ProdutoId == produto.Id &&
				x.Quantidade == carrinhoDto.Quantidade &&
				x.PrecoUnitario == produto.PrecoUnitario &&
				x.PrecoTotal == 31.00m)))
				.MustHaveHappenedOnceExactly();
		}
		#endregion

		#region Cadastrar_DadosValidos_DeveBaixarEstoqueProduto
		[Theory]
		[AutoData]
		public async Task Cadastrar_DadosValidos_DeveBaixarEstoqueProduto(CarrinhoDto carrinhoDto, Produto produto)
		{
			IRepProduto repProduto = A.Fake<IRepProduto>();
			IRepCarrinho repCarrinho = A.Fake<IRepCarrinho>();

			carrinhoDto.ProdutoId = produto.Id;
			carrinhoDto.Quantidade = 3;

			produto.Quantidade = 10;
			produto.PrecoUnitario = 20.00m;

			A.CallTo(() => repProduto.BuscarPorId(carrinhoDto.ProdutoId))
				.Returns(produto);

			ServCarrinho servCarrinho = new ServCarrinho(repProduto, repCarrinho);

			await servCarrinho.Cadastrar(carrinhoDto);

			Assert.Equal(7, produto.Quantidade);

			A.CallTo(() => repProduto.Editar(A<Produto>.That.Matches(x =>
				x.Id == produto.Id &&
				x.Quantidade == 7)))
				.MustHaveHappenedOnceExactly();
		}
		#endregion

		#region Listar_ComItens_DeveRetornarPrecoTotalSomado
		[Theory]
		[AutoData]
		public async Task Listar_ComItens_DeveRetornarPrecoTotalSomado(Carrinho carrinhoUm, Carrinho carrinhoDois)
		{
			IRepProduto repProduto = A.Fake<IRepProduto>();
			IRepCarrinho repCarrinho = A.Fake<IRepCarrinho>();

			carrinhoUm.PrecoTotal = 30.00m;
			carrinhoDois.PrecoTotal = 45.00m;

			List<Carrinho?> carrinhos = new List<Carrinho?>
			{
				carrinhoUm,
				carrinhoDois
			};

			A.CallTo(() => repCarrinho.Listar())
				.Returns(carrinhos);

			ServCarrinho servCarrinho = new ServCarrinho(repProduto, repCarrinho);

			CarrinhoResumoDto? carrinhoResumoDto = await servCarrinho.Listar();

			Assert.NotNull(carrinhoResumoDto);
			Assert.Equal(2, carrinhoResumoDto.Itens.Count);
			Assert.Equal(75.00m, carrinhoResumoDto.PrecoTotal);
		}
		#endregion

		#region Editar_CarrinhoInexistente_DeveLancarRegraDeNegocioException
		[Theory]
		[AutoData]
		public async Task Editar_CarrinhoInexistente_DeveLancarRegraDeNegocioException(Guid id, CarrinhoEditarDto carrinhoEditarDto)
		{
			IRepProduto repProduto = A.Fake<IRepProduto>();
			IRepCarrinho repCarrinho = A.Fake<IRepCarrinho>();

			A.CallTo(() => repCarrinho.BuscarPorId(id))
				.Returns(Task.FromResult<Carrinho>(null!));

			ServCarrinho servCarrinho = new ServCarrinho(repProduto, repCarrinho);

			RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servCarrinho.Editar(id, carrinhoEditarDto));

			Assert.Equal("Carrinho não encontrado", exception.Message);

			A.CallTo(() => repCarrinho.Editar(A<Carrinho>._))
				.MustNotHaveHappened();
		}
		#endregion

		#region Editar_DadosValidos_DeveRecalcularPrecoTotal
		[Theory]
		[AutoData]
		public async Task Editar_DadosValidos_DeveRecalcularPrecoTotal(Guid id, Carrinho carrinho, Produto produto, CarrinhoEditarDto carrinhoEditarDto)
		{
			IRepProduto repProduto = A.Fake<IRepProduto>();
			IRepCarrinho repCarrinho = A.Fake<IRepCarrinho>();

			carrinho.Id = id;
			carrinho.ProdutoId = produto.Id;
			carrinho.Quantidade = 2;
			carrinho.PrecoUnitario = 10.00m;
			carrinho.PrecoTotal = 20.00m;

			produto.Id = carrinho.ProdutoId;
			produto.Quantidade = 10;

			carrinhoEditarDto.Quantidade = 5;

			A.CallTo(() => repCarrinho.BuscarPorId(id))
				.Returns(carrinho);

			A.CallTo(() => repProduto.BuscarPorId(carrinho.ProdutoId))
				.Returns(produto);

			A.CallTo(() => repCarrinho.Editar(A<Carrinho>._))
				.ReturnsLazily((Carrinho carrinhoEditado) => carrinhoEditado);

			ServCarrinho servCarrinho = new ServCarrinho(repProduto, repCarrinho);

			Carrinho carrinhoResultado = await servCarrinho.Editar(id, carrinhoEditarDto);

			Assert.Equal(5, carrinhoResultado.Quantidade);
			Assert.Equal(50.00m, carrinhoResultado.PrecoTotal);

			A.CallTo(() => repProduto.Editar(A<Produto>._))
				.MustHaveHappenedOnceExactly();

			A.CallTo(() => repCarrinho.Editar(A<Carrinho>.That.Matches(x =>
				x.Id == id &&
				x.Quantidade == 5 &&
				x.PrecoTotal == 50.00m)))
				.MustHaveHappenedOnceExactly();
		}
		#endregion

		#region Remover_CarrinhoInexistente_DeveLancarRegraDeNegocioException
		[Theory]
		[AutoData]
		public async Task Remover_CarrinhoInexistente_DeveLancarRegraDeNegocioException(Guid id)
		{
			IRepProduto repProduto = A.Fake<IRepProduto>();
			IRepCarrinho repCarrinho = A.Fake<IRepCarrinho>();

			A.CallTo(() => repCarrinho.BuscarPorId(id))
				.Returns(Task.FromResult<Carrinho>(null!));

			ServCarrinho servCarrinho = new ServCarrinho(repProduto, repCarrinho);

			RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servCarrinho.Remover(id));

			Assert.Equal("Carrinho não encontrado", exception.Message);

			A.CallTo(() => repCarrinho.Remover(A<Guid>._))
				.MustNotHaveHappened();
		}
		#endregion
	}
}