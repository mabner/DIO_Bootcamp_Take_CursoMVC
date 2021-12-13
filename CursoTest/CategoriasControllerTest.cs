using CursoAPI.Controllers;
using CursoMVC.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CursoTest
{
    public class CategoriasControllerTest
    {
        private readonly Mock<DbSet<Categoria>> _mockSet;
        private readonly Mock<Context> _mockContext;
        private readonly Categoria _categoria;

        public CategoriasControllerTest()
        {
            // Mock database
            _mockSet = new Mock<DbSet<Categoria>>();
            _mockContext = new Mock<Context>();
            // Mock data for the test
            _categoria = new Categoria { Id = 1, Descricao = "Teste Categoria" };

            _mockContext.Setup(m => m.Categorias).Returns(_mockSet.Object);
            // Defines FindAsync
            _mockContext.Setup(m => m.Categorias.FindAsync(1)).ReturnsAsync(_categoria);
        }

        [Fact]
        public async Task Get_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);

            await service.GetCategoria(1);

            _mockSet.Verify(m => m.FindAsync(1), Times.Once());
        }
    }
}
