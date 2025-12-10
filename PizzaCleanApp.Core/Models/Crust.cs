using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCleanApp.Core.Models;

public class Crust : BaseEntity
{
    public static class MaxLength
    {
        public const int Name = 100;
    }
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int Calories { get; set; }
}
