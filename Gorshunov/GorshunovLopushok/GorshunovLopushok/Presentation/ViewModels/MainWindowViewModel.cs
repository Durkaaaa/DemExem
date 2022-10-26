using GorshunovLopushok.Domain.Entities;
using GorshunovLopushok.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorshunovLopushok.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<Product> Products { get; private set; }
        public MainWindowViewModel()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Products = db.Products
                .Include(pt => pt.ProductType)
                .Include(p => p.ProductMaterials)
                .ThenInclude(mp => mp.Material)
                .ToList();
            }
        }
    }
}
