using GorshunovLopushok.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GorshunovLopushok.Domain.Entities
{
    public partial class Product
    {
        private string? _image = null!;
        private decimal _cost;
        private string _materials = null!;
        private StringBuilder _stringBuilder = new();

        public Product()
        {
            ProductCostHistories = new HashSet<ProductCostHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            ProductSales = new HashSet<ProductSale>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? ProductTypeId { get; set; }
        public string ArticleNumber { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image
        {
            get => (_image == null) || (_image == string.Empty)
                ? $"\\Resources\\picture.png"
                : $"\\Resources{_image}";
            set => _image = value;
        }
        public int? ProductionPersonCount { get; set; }
        public int? ProductionWorkshopNumber { get; set; }
        public decimal MinCostForAgent { get; set; }


        [NotMapped]
        public string? FullName
        {
            get
            {
                return $"{ProductType!.Title}"+ " | " +$"{Title}";
            }
        }

        [NotMapped]
        public decimal Cost
        {
            get
            {
                if (ProductMaterials.Count() == 0)
                    return MinCostForAgent;

                foreach (var pm in ProductMaterials)
                {
                    _cost += Math.Ceiling((decimal)pm.Count! * pm.Material.Cost);
                }
;
                return _cost;
            }
        }

        [NotMapped]
        public string Materials
        {
            get 
            {
                _stringBuilder.Clear();
                if (ProductMaterials.Count() == 0)
                    return null!;

                _stringBuilder.Append("Материалы: ");
                foreach (var pm in ProductMaterials)
                {
                    _stringBuilder.Append(pm.Material.Title + ", ");
                }
                _stringBuilder.Remove(_stringBuilder.Length - 2, 2); ;
                return _stringBuilder.ToString(); 
            }
        }



        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
    }
}
