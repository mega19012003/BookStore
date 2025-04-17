﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WEBAGAIN.Data;
using WEBAGAIN.ViewModels;

namespace WEBAGAIN.ViewComponents
{
    public class MenuGenresViewComponent : ViewComponent
    {
        private readonly GameConsoleContext db;

        public MenuGenresViewComponent(GameConsoleContext context) => db = context;

     public IViewComponentResult Invoke()
        {
            var data = db.Genres.Select(g => new MenuGenresVM
            {
                MaGenre = g.GenreId, 
                TenGenre = g.GenreName, 
                SoLuong = g.Games.Count
            }).OrderBy(p => p.TenGenre); ;

            return View(data);
        }
    }
}
