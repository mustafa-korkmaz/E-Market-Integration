﻿using Api.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Api.DAL.DTO
{
    public class Integration
    {
        public int Id { get; set; }

        [Required]
        [Index("IX_Name", IsUnique = true)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public IntegrationType Type { get; set; }

        [Required]
        public IntegrationStatus Status { get; set; }

        public ICollection<MarketUserIntegration> MarketUserIntegrations { get; set; } // 1=>n relation
    
        public ICollection<IntegrationDetail> IntegrationDetails { get; set; } // 1=>n relation

    }
}