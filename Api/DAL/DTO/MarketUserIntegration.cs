using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.DAL.DTO
{
    public class MarketUserIntegration
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Desc { get; set; }

        [Required]
        public int MarketUserId { get; set; }

        [Required]
        public int IntegrationId { get; set; }

        public Integration Integration { get; set; } // navigation property
    }
}