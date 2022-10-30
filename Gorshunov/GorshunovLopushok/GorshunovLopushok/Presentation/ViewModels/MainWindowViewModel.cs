using GorshunovLopushok.Domain.Entities;
using GorshunovLopushok.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GorshunovLopushok.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<Product> _products = new();
        private int _selectedSorting;
        private int _selectedFiltering;
        private string _search = null!;

        public List<string> SortingList
        {
            get
            {
                List<string> list = new()
                {
                    "Без Сортировки",
                    "По названию (А-Я)",
                    "По названию (Я-А)",
                    "По типу",
                    "По артиклу (min-max)",
                    "По артиклу (max-min)",
                    "По стоимости (min-max)",
                    "По стоимости (max-min)"
                };
                return list;
            }
        }

        public List<string> FilteringList
        {
            get
            {
                List<string> list = new()
                {
                    "Без Фильтрации"
                };

                foreach (var item in new ApplicationDbContext().ProductTypes)
                {
                    list.Add(item.Title);
                }
                return list;
            }
        }

        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
            }
        }

        public int SelectedSorting
        {
            get => _selectedSorting;
            set
            {
                _selectedSorting = value;
                switch (value)
                {
                    case 0:
                        Products = GetProducts()
                            .Where(item => Products.Any(t => t.Title == item.Title))
                            .ToList();
                        break;
                    case 1:
                        Products = Products
                            .OrderBy(t => t.Title)
                            .ToList();
                        break;
                    case 2:
                        Products = Products
                            .OrderByDescending(t => t.Title)
                            .ToList();
                        break;
                    case 3:
                        Products = Products
                            .OrderBy(pt => pt.ProductType!.Title)
                            .ToList();
                        break;
                    case 4:
                        Products = Products
                            .OrderBy(an => an.ArticleNumber)
                            .ToList();
                        break;
                    case 5:
                        Products = Products
                            .OrderByDescending(an => an.ArticleNumber)
                            .ToList();
                        break;
                    case 6:
                        Products = Products
                            .OrderBy(c => c.Cost)
                            .ToList();
                        break;
                    case 7:
                        Products = Products
                            .OrderByDescending(c => c.Cost)
                            .ToList();
                        break;
                }
            }
        }

        public int SelectedFiltering
        {
            get => _selectedFiltering;
            set
            {
                _selectedFiltering = value;
                switch (value)
                {
                    case 0:
                        Products = GetProducts();
                        break;

                    default:
                        Products = GetProducts()
                            .Where(p => p.ProductType!.Id == new ApplicationDbContext().ProductTypes.FirstOrDefault(t => t.Title == FilteringList[value])!.Id)
                            .ToList();
                        break;
                }
                SelectedSorting = _selectedSorting;
            }
        }


        public MainWindowViewModel()
        {
            Products = GetProducts();
        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products = new ApplicationDbContext().Products
                .Include(pt => pt.ProductType)
                .Include(p => p.ProductMaterials)
                .ThenInclude(mp => mp.Material)
                .ToList();
            return products;
        }
    }
}
