using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace PCConfigurator.Models
{
    public class Case : Component
    {
        // В базата ще го пазим като текст: "ATX,mATX"
        public string SupportedFormFactors { get; set; } = string.Empty;

        // Помощно свойство за кода (не отива в базата)
        [NotMapped]
        public List<string> FormFactorsList
        {
            get => SupportedFormFactors.Split(',').ToList();
        }
    }
}