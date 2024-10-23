using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.InputModels
{
    public class TourVoucherInsertModel
    {
        public string TourVoucherCode { get; set; } = "";
        public IFormFile? VoucherFile { get; set; }
    }
}
