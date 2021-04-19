using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _productRepository = new FakeProductRepository();
            _orderRepository = new FakeOrderRepository();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmClienteInexistenteOPedidoNaoDeveSerGerado()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmCEPInvalidoOPedidoNaoDeveSerGerado()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmPromocodeInexistenteOPedidoDeveSerGeradoNormalmente()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmPedidoSemItensOMesmoNaoDeveSerGerado()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            Assert.Fail();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoValidoOPedidoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678911";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);
            Assert.IsTrue(handler.Valid);
        }
    }
}
