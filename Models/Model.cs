using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace tysjyfgjkhfghjetsrstr.Models
{
    public class Model 
    {
        public string SelectedOptionfrom { get; set; }
        public string SelectedOptionto { get; set; }
        public List<SelectListItem> OptionsList { get; set; }
        public Model()
        
        {
            OptionsList = new List<SelectListItem>
            {
        new SelectListItem { Value = "Warszawa-Centralna", Text = "Warszawa Centralna" },
        new SelectListItem { Value = "Krakow-Glowny", Text = "Kraków Główny" },
        new SelectListItem { Value = "Gdansk-Glowny", Text = "Gdańsk Główny" },
        new SelectListItem { Value = "Wroclaw-Glowny", Text = "Wrocław Główny" },
        new SelectListItem { Value = "Poznan-Glowny", Text = "Poznań Główny" },
        new SelectListItem { Value = "Lodz-Fabryczna", Text = "Łódź Fabryczna" },
        new SelectListItem { Value = "Katowice", Text = "Katowice" },
        new SelectListItem { Value = "Szczecin-Glowny", Text = "Szczecin Główny" },
        new SelectListItem { Value = "Bydgoszcz-Glowna", Text = "Bydgoszcz Główna" },
        new SelectListItem { Value = "Bialystok", Text = "Białystok" }
    

        };
        }
        
    }

}
