using SalesWebMvc.Models;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private readonly SalesWebMvcContext _context;

        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Verifica se o banco já foi populado
            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
            {
                return; // O banco já foi populado
            }

            // Criação dos departamentos
            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            // Criação dos vendedores (associados aos departamentos)
            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 1000.0, d1);
            Seller s2 = new Seller(2, "Maria Green", "maria@gmail.com", new DateTime(1979, 12, 31), 1000.0, d2);
            Seller s3 = new Seller(3, "Alex Grey", "alex@gmail.com", new DateTime(1988, 1, 15), 1000.0, d3);
            Seller s4 = new Seller(4, "Martha Red", "martha@gmail.com", new DateTime(1993, 11, 30), 1000.0, d4);
            Seller s5 = new Seller(5, "Donald Blue", "donald@gmail.com", new DateTime(2000, 1, 9), 1000.0, d1);
            Seller s6 = new Seller(6, "Alex Pink", "alex@gmail.com", new DateTime(1997, 3, 4), 1000.0, d2);

            // Criação dos registros de vendas (associados aos vendedores)
            SalesRecord sr1 = new SalesRecord(1, new DateTime(2018, 12, 1), 500.0, Models.Enums.SaleStatus.Billed, s1);
            SalesRecord sr2 = new SalesRecord(2, new DateTime(2024, 12, 10), 1000.0, Models.Enums.SaleStatus.Pending, s2);
            SalesRecord sr3 = new SalesRecord(3, new DateTime(2024, 10, 1), 950.0, Models.Enums.SaleStatus.Canceled, s3);

            // Adiciona os dados ao contexto
            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);
            _context.SalesRecord.AddRange(sr1, sr2, sr3);

            // Salva as alterações no banco de dados
            _context.SaveChanges();
        }
    }
}
