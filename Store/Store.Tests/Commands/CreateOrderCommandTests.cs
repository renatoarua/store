using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Entities;

namespace Store.Tests.Entities
{
    [TestClass]
    public class CreateOrderCommandTests
    {
        [TestMethod]
        [TestCategory("Handlers")]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.IsFalse(command.Valid);
        }
    }
}
