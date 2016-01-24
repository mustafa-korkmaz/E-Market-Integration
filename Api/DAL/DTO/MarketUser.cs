using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.DAL.DTO
{
    public class MarketUser
    {
        public int Id { get; set; }
        public int MarketId { get; set; } // foreign key for market
        public Market Market { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("IX_Name", IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string LoginName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Token { get; set; }

        public ICollection<MarketUserIntegration> MarketUserIntegrations { get; set; } // 1=>n relation

    }
}