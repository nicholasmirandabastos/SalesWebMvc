using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year - 1,1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");


            // Obtém os registros de vendas filtrados
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);

            // Calcula o total das vendas
            var totalSales = result.Sum(sr => sr.Amount);

            // Passa o total de vendas e os filtros para a ViewData
            ViewData["TotalSales"] = totalSales.ToString("F2");
            ViewData["minDate"] = minDate?.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate?.ToString("yyyy-MM-dd");

            return View(result); // Passa a lista de registros para a view
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}
