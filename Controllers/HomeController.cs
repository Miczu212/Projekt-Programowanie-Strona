using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using tysjyfgjkhfghjetsrstr.Data;
using tysjyfgjkhfghjetsrstr.Models;

namespace tysjyfgjkhfghjetsrstr.Controllers
{
    public class HomeController : Controller   
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            var model = new Model();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(Model model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Wyniki", new { SelectedOptionfrom = model.SelectedOptionfrom, SelectedOptionto = model.SelectedOptionto });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Wyniki(string SelectedOptionfrom, string SelectedOptionto)
        {
            bool czybreak = false; //potrzebny zeby wyjsc z podwojnej petli
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-tysjyfgjkhfghjetsrstr-3210E9E5-1168-47D3-A227-16692EF6DF66;Trusted_Connection=True;MultipleActiveResultSets=true";
            List<string> visitedStations = new List<string>();//potrzebne do wyszukiwania polaczen posrednich
            List<polaczenia> listapolaczen = new List<polaczenia>();
            bool czyprzesiadka = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query1 = $"SELECT Stacja1, Stacja2 FROM polaczenia WHERE Stacja1 = '{SelectedOptionfrom}' AND Stacja2 = '{SelectedOptionto}'";
                SqlCommand command1 = new SqlCommand(query1, connection);
                SqlDataReader reader1 = command1.ExecuteReader();

                if (reader1.HasRows)
                {
                    czyprzesiadka = false;
                    polaczenia directConnection = new polaczenia
                    {
                        stacja1 = SelectedOptionfrom,
                        stacja2 = SelectedOptionto
                    };
                    listapolaczen.Add(directConnection);
                }
                else
                {
                    czyprzesiadka = true;   //informuje widok zeby napisal ze nie ma bezposredniego polaczenia
                    Queue<string> queue = new Queue<string>();
                    queue.Enqueue(SelectedOptionfrom);
                    visitedStations.Add(SelectedOptionfrom);

                    while (!czybreak && queue.Count>0)
                    {
                        
                        string temp = queue.Dequeue();
                        string query2 = $"SELECT Stacja1, Stacja2 FROM polaczenia WHERE Stacja1 = '{temp}'";
                        SqlCommand command2 = new SqlCommand(query2, connection);
                        SqlDataReader reader2 = command2.ExecuteReader();

                        while (reader2.Read())
                        {
                            string startStation = reader2["Stacja1"].ToString();
                            string destinationStation = reader2["Stacja2"].ToString();

                            if (!visitedStations.Contains(destinationStation))
                            {
                                visitedStations.Add(destinationStation);
                                queue.Enqueue(destinationStation);

                                polaczenia intermediateConnection = new polaczenia
                                {
                                    stacja1 = startStation,
                                    stacja2 = destinationStation
                                };
                                listapolaczen.Add(intermediateConnection);
                                if (destinationStation == SelectedOptionto)
                                {
                                    czybreak = true;
                                    break;
                                }
                                
                            }
                        }                      
                    }
                    if (queue.Count <= 0)                    
                        listapolaczen.Clear();
                    
                }

                reader1.Close();
            }
            listapolaczenia lista = new listapolaczenia(listapolaczen);
            lista.czyprzesiadka = czyprzesiadka;
            return View("Wyniki",lista);
        }

        //notatka do samego siebie, wywal sie w strone wyniki i zrob handlowanie na indexie
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}