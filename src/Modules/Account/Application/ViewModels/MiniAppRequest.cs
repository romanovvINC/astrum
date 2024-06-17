using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Astrum.Storage.ViewModels;

namespace Astrum.Account.Application.ViewModels
{
    public class MiniAppRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название мини-приложения.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите ссылку на мини-приложение.")]
        public string Link { get; set; }
        public FileForm Image { get; set; }
    }
}
