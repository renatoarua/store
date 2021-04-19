using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("Renato", "re@gmail.com");
        private readonly Product _product = new Product("Mouse", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));
        private readonly Order _order;

        public OrderTests()
        {
            _order = new Order(_customer, 10, _discount);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPedidovalidoEleDeveGerarUmNumeroCom8Caracteres()
        {
            Assert.AreEqual(8, _order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoPedidoSeuStatusDeveSerAguardandoPagamento()
        {
            Assert.AreEqual(EOrderStatus.WatingPayment, _order.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPagamentoDoPedidoSeuStatusDeveSerAguardandoEntrega()
        {
            _order.AddItem(_product, 1);
            _order.Pay(10);
            Assert.AreEqual(EOrderStatus.WaitingDelivery, _order.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmpedidoCanceladoSeuStatusDeveSerCancelado()
        {
            _order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoItemSemProdutoOMesmoNaoDeveSerAdicionado()
        {
            _order.AddItem(null, 1);
            Assert.AreEqual(0, _order.Items.Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoItemComQuantidadeZeroOuMenorOMesmoNaoDeveSerAdicionado()
        {
            _order.AddItem(_product, 0);
            Assert.AreEqual(0, _order.Items.Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmNovoPedidoValidoSeuTotalDeveSer50()
        {
            _order.AddItem(_product, 5);
            Assert.AreEqual(50, _order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontoExpiradoOValorDoPedidoDeveSer60()
        {
            var discount = new Discount(10, DateTime.Now.AddDays(-5));
            var order = new Order(_customer, 10, discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(60, order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontoInvalidoOValorDoPedidoDeveSer60()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);
            Assert.AreEqual(60, order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmDescontoDe10OValorDoPedidoDeveSer50()
        {
            _order.AddItem(_product, 5);
            Assert.AreEqual(50, _order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmaTaxaDeEntregaDe20OValorDoPedidoDeveSer60()
        {
            _order.AddItem(_product, 6);
            Assert.AreEqual(60, _order.Total());
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void DadoUmPedidoSemClienteOMesmoDeveSerInvalido()
        {
            var order = new Order(null, 10, _discount);
            Assert.IsFalse(order.Valid);
        }
    }
}
